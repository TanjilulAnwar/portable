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
import CustomerForm from '../../../components/forms/configuration/customer';
import { customerList } from '../server_action';
import { PrintButton } from 'components/PDF';


const row = {paddingTop: 2, paddingBottom: 2}
const columns = [
  { key: 'name', title: 'Name', isResizable: true, style:{...row}},
  { key: 'patient_id', title: 'Patient ID', isResizable: true, style:{...row}},
  { key: 'mobile', title: 'Mobile No.', isResizable: true, style:{...row}},
  { key: 'address', title: 'Address', isResizable: true, style:{...row}},
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

const AddOrUpdate = React.memo(({open, handleClose, updateList, update, customer_info})=>{
  return(
    <Dialog
      open={open}
      onClose={handleClose}
      aria-labelledby="dialog-title"
      aria-describedby="dialog-description"
      scroll="body"
    >
      <DialogTitle className="text-center" id="dialog-title">{update?'Update Customer':'Add Customer'}</DialogTitle>
      <DialogContent>
        <CustomerForm updateList={updateList} update={update} customer_info={customer_info} handleClose={handleClose}/>
      </DialogContent>
    </Dialog>
  )
})



const CustomerEntry = ()=>{
  const [open, setOpen] = React.useState(false)
  const [tableProps, changeTableProps] = React.useState(tablePropsInit);
  const [customer_info, setCustomerInfo] = React.useState({})
  const [update, setUpdate] = React.useState(false)

  const dispatch = React.useCallback((action) => {
    if(action.type === 'EDIT'){
      setOpen(true)
      setCustomerInfo(action.rowData)
      setUpdate(true)
    } else if(action.type === 'ADD') {
      setOpen(true)
      setCustomerInfo({})
      setUpdate(false)
    }
    changeTableProps((prevState) => kaReducer(prevState, action));
  }, [changeTableProps, setOpen])

  React.useEffect(()=>{
    dispatch(showLoading('Getting Data ...'))
    customerList()
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
      <h5 className="text-center font-weight-bold">Customers</h5>
      <div className="card overflow-hidden">
        <Table
          {...tableProps}
          dispatch={dispatch}
          childComponents={{
            filterRowCell: {
              content: (props) => {
                switch(props.column.key){
                  case 'address': return <React.Fragment/>
                  case 'action': return <PrintButton url="/print/Customer"/>
                  default: return
                }
              }
            },
            cellText: {
              content: props => {
                switch(props.column.key){
                  case 'address': return <p className="text-truncate m-0">{`${props.rowData.address}, ${props.rowData.thana}, ${props.rowData.district}, ${props.rowData.division}`}</p>
                  case 'action': return <ActionButton {...props} />
                  default: return <p className="text-truncate m-0">{props.value}</p>
                }
              }
            },
            headCell: {
              content: props => {
                switch(props.column.key){
                  case 'action': return <AddButton {...props} />
                  default: return <div className="pt-2 text-primary">{props.column.title}</div>
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
        customer_info={customer_info}
      />

    </React.Fragment>
  )
}

export default CustomerEntry;