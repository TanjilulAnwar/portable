import React from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
} from '@material-ui/core';
import { kaReducer, Table } from 'ka-table';
import { FilteringMode } from 'ka-table/enums';
import { updateData, hideLoading, showLoading } from 'ka-table/actionCreators';
import { ActionButton, AddButton } from '../../../components/table-component';
import ProductForm from 'components/forms/product-configuration/product-form';
import { productList } from '../server_action';
import { PrintButton } from 'components/PDF';


const row = {paddingTop: 2, paddingBottom: 2}
const columns = [
  { key: 'product_name', title: 'Name', isResizable: true, style:{...row, width: 220}},
  { key: 'manufacturer', title: 'Manufacturer', isResizable: true, style:{...row, width: 200}},
  { key: 'category', title: 'Generic Name', isResizable: true, style:{...row, width: 200}},
  { key: 'quantity', title: 'Quantity', style:{...row, width: 100, textAlign: 'center'}},
  { key: 'unit_price', title: 'Unit Price', isResizable: true, style:{...row, width: 120, textAlign: 'center'}},
  { key: 'mrp_price', title: 'MRP', style:{...row, width: 100, textAlign: 'center'}},
  { key: 'action', style:{...row, width: 80}},
]

const tablePropsInit = {
  columns: columns,
  data: [],
  rowKeyField: 'id',
  filteringMode: FilteringMode.FilterRow,
  paging: {
    enabled: true,
    pageIndex: 0,
    pageSize: 20,
  }
}

const AddOrUpdate = React.memo(({open, handleClose, updateList, update, manufacturer_info})=>{
  return(
    <Dialog
      open={open}
      onClose={handleClose}
      aria-labelledby="dialog-title"
      aria-describedby="dialog-description"
      scroll="body"
    >
      <DialogTitle className="text-center" id="dialog-title">{update?'Update Product':'Add New Product'}</DialogTitle>
      <DialogContent>
        <ProductForm updateList={updateList} update={update} manufacturer_info={manufacturer_info} handleClose={handleClose}/>
      </DialogContent>
    </Dialog>
  )
})



const ProductList = ()=>{
  const [open, setOpen] = React.useState(false)
  const [tableProps, changeTableProps] = React.useState(tablePropsInit);
  const [manufacturer_info, setManufacturerInfo] = React.useState({})
  const [update, setUpdate] = React.useState(false)

  const dispatch = React.useCallback((action) => {
    if(action.type === 'EDIT'){
      setOpen(true)
      setManufacturerInfo(action.rowData)
      setUpdate(true)
    } else if(action.type === 'ADD') {
      setOpen(true)
      setManufacturerInfo({})
      setUpdate(false)
    }
    changeTableProps((prevState) => kaReducer(prevState, action));
  }, [changeTableProps, setOpen])

  React.useEffect(()=>{
    dispatch(showLoading('Getting Data ...'))
    productList()
      .then(resp => {
        if(resp.success){
          dispatch(updateData(resp.message))
        }
      })
      .finally(()=>dispatch(hideLoading()))
  }, [dispatch])

  const handleClose = React.useCallback(()=> setOpen(false), [setOpen])

  const updateList = React.useCallback((data, type="ADD_NEW")=>{
    const new_list = [...tableProps.data]
    if(type === "ADD_NEW"){
      new_list.push(data)
    } else if(type === "UPDATE") {
      const index = new_list.findIndex(val => val.id === data.id)
      new_list[index] = data
      handleClose()
    }
    dispatch(updateData(new_list))
  }, [tableProps, dispatch, handleClose])


  return(
    <React.Fragment>
      <h5 className="text-center font-weight-bold">Products</h5>
      <div className="card overflow-hidden">
        <Table
          {...tableProps}
          dispatch={dispatch}
          childComponents={{
            filterRowCell: {
              content: (props) => {
                switch(props.column.key){
                  case 'action': return <div className="d-flex justify-content-center"><PrintButton url="/print/Products"/></div>
                  default: return
                }
              }
            },
            cellText: {
              content: props => {
                switch(props.column.key){
                  case 'action': return <ActionButton {...props} />
                  default: return <p className="text-truncate m-0">{props.value}</p>
                }
              }
            },
            headCell: {
              content: props => {
                switch(props.column.key){
                  case 'action': return <AddButton {...props} />
                  default: return <div className="text-primary">{props.column.title}</div>
                }
              },
            }
          }}
        />
      </div>

      <AddOrUpdate
        open={open}
        handleClose={handleClose}
        updateList={updateList}
        update={update}
        manufacturer_info={manufacturer_info}
      />

    </React.Fragment>
  )
}

export default ProductList;