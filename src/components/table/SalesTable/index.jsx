import React from 'react';
import { kaReducer, Table } from 'ka-table';
import { EditingMode, DataType } from 'ka-table/enums';
import { updateData } from 'ka-table/actionCreators';
import cogoToast from 'cogo-toast';
import { DeleteButton } from '../../../components/table-component';
import AddProduct from 'components/forms/sales/AddProduct';
import BarcodeReader from 'react-barcode-reader';
import {productSaleinformation} from 'pages/sales/server_action';


const padding = {paddingTop: 2, paddingBottom:2}
const tableProps = {
  columns: [
    { key: 'product_code', title: 'Code', isEditable: false, visible: false, style:{...padding} },
    { key: 'product_name', title: 'Product Name', isEditable: false, style:{...padding} },
    { key: 'category_name', title: 'Category', isEditable: false, style:{...padding} },
    { key: 'mrp_price', title: 'MRP', isEditable: false, style: {...padding, width: 140, textAlign: 'center'} },
    { key: 'quantity', title: 'Quantity', dataType: DataType.Number, style: {...padding, width: 110, textAlign: 'center'} },
    { key: 'discount', title: 'Discount (%)', dataType: DataType.Number, style: {...padding, width: 110, textAlign: 'center'} },
    { key: 'total_price', title: 'Total', isEditable: false, style: {...padding, width: 150, textAlign: 'center'} },
    { key: 'action',  title: 'Action', isEditable: false, style: {...padding, width: 90} },
  ],
  data: [],
  editingMode: EditingMode.Cell,
  rowKeyField: 'product_code',
};

class SalesTable extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      tableProps: {...tableProps},
    };
  }

  dispatch = (action)=>{
    if(action.type === 'OpenEditor' && action.columnKey === "action")
      return

    const new_data = this.state.tableProps
    if(action.type === 'UpdateCellValue'){
      const index = new_data.data.findIndex(val => val.product_code === action.rowKeyValue)
      if(action.columnKey === "quantity"){
        action.value = parseInt(action.value) > 0 ? parseInt(action.value) : 1
        let total = new_data.data[index]['mrp_price'] * action.value
        new_data.data[index]['total_price'] = total - (total*new_data.data[index]['discount']/100)
        new_data.data[index]['quantity'] = action.value
      } else if(action.columnKey === "discount") {
        action.value = parseInt(action.value) > -1 ? parseInt(action.value) : 0
        let total = new_data.data[index]['mrp_price'] * new_data.data[index]['quantity']
        new_data.data[index]['total_price'] = total - (total*action.value/100)
        new_data.data[index]['discount'] = action.value
      }
      this.props.updateSaleList(new_data.data)
    }
    const update_table = kaReducer(new_data, action)
    this.setState({tableProps: update_table});
    if(action.type === 'UpdateData' || action.type === 'DeleteRow'){
      this.props.updateSaleList(update_table.data)
    }
  }

  componentDidMount(){
    this.dispatch(updateData([]))
  }


  handleScan = (data, quantity, discount)=>{
    const table_data = this.state.tableProps.data
    const index = table_data.findIndex(val => val.product_code === data)
    if(index === -1){
      productSaleinformation(data)
        .then(resp=>{
          if(resp.success){
            let pro_info = resp.message
            pro_info['quantity'] = parseInt(quantity) || 1
            pro_info['discount'] = parseFloat(discount) || 0
            pro_info['total_price'] = pro_info['quantity']*pro_info.mrp_price
            pro_info['total_price'] = pro_info['total_price']-(pro_info['total_price']*pro_info['discount']/100)
            const new_data = [pro_info, ...table_data]
            this.dispatch(updateData(new_data));
          } else cogoToast.error(resp.message)
        })
    } else {
      table_data[index]['quantity'] += 1
      let total = table_data[index]['mrp_price'] * table_data[index]['quantity']
      table_data[index]['total_price'] = total - (total*table_data[index]['discount']/100)
      this.dispatch(updateData(table_data));
    }
  }

  addToTable = (product)=>{
    this.handleScan(product.product_code, product.quantity, product.discount)
  }


  render() {
    return (
      <React.Fragment>
        <AddProduct addToTable={this.addToTable}/>
        <div className="card p-0 overflow-hidden">
          <BarcodeReader  onScan={this.handleScan}/>
          <Table
            {...this.state.tableProps}
            dispatch={this.dispatch}
            childComponents={{
              headCell: {
                content: props => {
                  switch(props.column.key){
                    default: return <div className="text-primary">{props.column.title}</div>
                  }
                },
              },
              cellText: {
                content: props => {
                  switch(props.column.key){
                    case 'total_price': return props.rowData.discount>0
                                                ? <div>(<del>{props.rowData.mrp_price*props.rowData.quantity}</del>) {props.value}</div>
                                                : <div>{props.value}</div>
                    case 'action': return <DeleteButton {...props} />
                    default: return
                  }
                }
              },
            }}
          />
        </div>
      </React.Fragment>
    );
  }
}

export default SalesTable;