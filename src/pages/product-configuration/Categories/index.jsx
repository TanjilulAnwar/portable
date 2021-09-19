import React from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
} from '@material-ui/core';
import { kaReducer, Table } from 'ka-table';
import { FilteringMode, SortingMode, SortDirection } from 'ka-table/enums';
import { updateData, hideLoading, showLoading } from 'ka-table/actionCreators';
import { ActionButton, AddButton } from '../../../components/table-component';
import CategoryForm from '../../../components/forms/product-configuration/category-form';
import { categoryList } from '../server_action';
import { PrintButton } from 'components/PDF';


const row = {paddingTop: 2, paddingBottom: 2}
const columns = [
  { key: 'name', title: 'Category Name', isResizable: true, style:{...row, width: 400}, sortDirection: SortDirection.Ascend},
  // { key: 'subcategories', title: 'Subcategories', style:{...row}},
  { key: 'description', title: 'Description', style:{...row}},
  { key: 'action', style:{...row, width: 80}},
]

const tablePropsInit = {
  columns: columns,
  data: [],
  rowKeyField: 'id',
  filteringMode: FilteringMode.FilterRow,
  sortingMode: SortingMode.Single,
  paging: {
    enabled: true,
    pageIndex: 0,
    pageSize: 15,
  }
}

const AddOrUpdate = React.memo(({open, handleClose, updateList, update, category_info})=>{
  return(
    <Dialog
      open={open}
      onClose={handleClose}
      aria-labelledby="dialog-title"
      aria-describedby="dialog-description"
      scroll="body"
      disableBackdropClick={true}
    >
      <DialogTitle className="text-center" id="dialog-title">{update?'Update Category':'Add Category'}</DialogTitle>
      <DialogContent>
        <CategoryForm updateList={updateList} update={update} category_info={category_info} handleClose={handleClose}/>
      </DialogContent>
    </Dialog>
  )
})


const Categories = ()=>{
  const [open, setOpen] = React.useState(false)
  const [tableProps, changeTableProps] = React.useState(tablePropsInit);
  const [category_info, setCategoryInfo] = React.useState({})
  const [update, setUpdate] = React.useState(false)

  const dispatch = React.useCallback((action) => {
    if(action.type === 'EDIT'){
      setOpen(true)
      setCategoryInfo(action.rowData)
      setUpdate(true)
    } else if(action.type === 'ADD') {
      setOpen(true)
      setCategoryInfo({})
      setUpdate(false)
    }
    changeTableProps((prevState) => kaReducer(prevState, action));
  }, [changeTableProps, setOpen])

  React.useEffect(()=>{
    dispatch(showLoading('Getting Data ...'))
    categoryList()
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
      <h5 className="text-center font-weight-bold">Categories</h5>
      <div className="card overflow-hidden">
        <Table {...tableProps}
          dispatch={dispatch}
          childComponents={{
            filterRowCell: {
              content: (props) => {
                switch(props.column.key){
                  case 'name': return
                  case 'action': return <PrintButton url="/Category/Print"/>
                  default: return <React.Fragment/>
                }
              }
            },
            cellText: {
              content: props => {
                switch(props.column.key){
                  case 'action': return <ActionButton {...props} />
                  case 'subcategories': return <p className="text-truncate m-0">{props.value && props.value.join(', ')}</p>
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
        category_info={category_info}
      />

    </React.Fragment>
  )
}

export default Categories;