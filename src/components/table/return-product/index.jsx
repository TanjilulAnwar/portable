import React from 'react';
import { kaReducer, Table } from 'ka-table';
import { EditingMode, DataType } from 'ka-table/enums';
import {SelectAll, SelectRow} from 'components/table-component';
import PrevTotal from './PrevTotal';
import NewTotal from './NewTotal';
import {returnProduct} from 'pages/ReturnProduct/server_action';
import cogoToast from 'cogo-toast';

const padding = {paddingTop: 2, paddingBottom: 2}
const tableProps = {
  columns: [
    { key: 'action', isEditable: false, style: {...padding, width: 50} },
    { key: 'product_code', title: 'Code', isEditable: false, visible: false, style: {...padding}},
    { key: 'product_name', title: 'Product Name', isEditable: false, style: {...padding}},
    { key: 'mrp_price', title: 'MRP', isEditable: false, style: {...padding, width: 140, textAlign: 'center'} },
    { key: 'quantity', title: 'Quantity', dataType: DataType.Number, style: {...padding, width: 110, textAlign: 'center'} },
    { key: 'discount', title: 'Discount (%)', isEditable: false, style: {...padding, width: 110, textAlign: 'center'} },
    { key: 'total_price', title: 'Total', isEditable: false, style: {...padding, width: 150, textAlign: 'center'} },
  ],
  data: [],
  selectedRows: [],
  editingMode: EditingMode.Cell,
  rowKeyField: 'product_code',
  validation: ({rowData, value})=>{
    const old_val = old_data.find(val => val.product_code===rowData.product_code)['quantity']
    if(value > old_val)
      return `Can't return more than ${old_val}`
  }
};

let old_data = []

class ProductTable extends React.Component {
  constructor(props) {
    super(props);
    old_data = this.props.product_info.purchase_list || this.props.product_info.sales_list
    tableProps.data = JSON.parse(JSON.stringify(old_data))
    this.state = {
      tableProps: {...tableProps},
      return_by: '',
      discount: 0,
      percent: true,
      total: 0,
      saving: false,
      saved: false
    };
  }

  dispatch = (action)=>{
    const new_data = this.state.tableProps
    if(action.type === 'UpdateCellValue'){
      const index = new_data.data.findIndex(val => val.product_code === action.rowKeyValue)
      if(action.columnKey === "quantity"){
        let value = parseInt(action.value)
        value = value > 0 ? value : 1
        let total = new_data.data[index]['mrp_price'] * value
        new_data.data[index]['total_price'] = total - (total*new_data.data[index]['discount']/100)
        new_data.data[index]['quantity'] = value
      }
    }
    const update_table = kaReducer(new_data, action)
    let total = 0
    update_table.selectedRows.forEach(code => {
      let pro = update_table.data.find(val => val.product_code === code)
      total += pro.total_price
    })
    this.setState({tableProps: update_table, total});
  }

  handleChange = (e)=> this.setState({[e.target.name]: e.target.name === 'discount' ? parseInt(e.target.value||0): e.target.value})
  handleDiscountType = ()=> this.setState({percent: !this.state.percent})

  handleSubmit = ()=>{
    this.setState({saving: true})
    let obj = {
      [!!this.props.product_info.supplier_code?'supplier_code':'customer_code']: this.props.product_info.supplier_code || this.props.product_info.customer_code,
      invoice: this.props.product_info.invoice,
      return_by: this.state.return_by,
      total: this.state.total,
      discount: this.state.discount,
      percent: this.state.percent
    }
    let pro_list = []
    this.state.tableProps.selectedRows.forEach(code => {
      pro_list.push(this.state.tableProps.data.find(val => val.product_code === code))
    })
    obj['product_list'] = pro_list
    returnProduct(obj, !!this.props.product_info.supplier_code)
     .then(resp => {
        if(resp.success){
          cogoToast.success('Saved successfully')
          this.setState({saved: true})
        }
        else cogoToast.error(resp.message)
     })
     .finally(()=>this.setState({saving: false}))
  }


  render() {
    return (
      <div className="d-block">
        <div className={this.props.product_info.returned && `_disabled-block`}>
          <div className="card p-0 pb-3 overflow-hidden">
            <Table
              {...this.state.tableProps}
              dispatch={this.dispatch}
              childComponents={{
                headCell: {
                  content: props => {
                    if (props.column.key === 'action'){
                      return (
                        <SelectAll {...props}
                          areAllRowsSelected={this.state.tableProps.data.length === this.state.tableProps.selectedRows.length}
                        />
                      );
                    } else return <div className="text-primary">{props.column.title}</div>
                  }
                },
                cellText: {
                  content: props => {
                    switch(props.column.key){
                      case 'total_price': return props.rowData.discount>0
                                                ? <div>(<del>{props.rowData.mrp_price*props.rowData.quantity}</del>) {props.value}</div>
                                                : <div>{props.value}</div>
                      case 'action': return <SelectRow {...props} />
                      default: return
                    }
                  }
                },
              }}
            />
          </div>
          <div className="form-row">
            <div className="col">
              <PrevTotal info={this.props.product_info}/>
            </div>
            <div className="col">
              <NewTotal
                total={this.state.total}
                percent={this.state.percent}
                handleDiscountType={this.handleDiscountType.bind(this)}
                return_by={this.state.return_by}
                handleChange={this.handleChange.bind(this)}
                handleSubmit={this.handleSubmit.bind(this)}
                saving={this.state.saving}
                saved={this.state.saved}
              />
            </div>
          </div>
        </div>

        {this.props.product_info.returned &&
          <div className="_disabled-block-text">
            <h2 className="text-center text-secondary">This invoice has been already used once for return</h2>
          </div>
        }

      </div>
    );
  }
}

export default ProductTable;