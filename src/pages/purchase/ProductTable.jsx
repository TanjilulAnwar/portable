import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import { DeleteButton } from 'components/table-component';
import AddProduct from 'components/forms/purchase/AddProduct';



export default React.memo(({handlePurchaseList})=>{
  const [purchase_list, setPurchaseList] = React.useState([])

  const updateList = React.useCallback((product)=> {
    let list = []
    list = [product, ...purchase_list]
    setPurchaseList(list)
    handlePurchaseList(list)
  }, [purchase_list, handlePurchaseList])

  
  const dispatch = React.useCallback((action) => {
    if(action.type === 'DeleteRow'){
      purchase_list.splice(action.rowKeyValue, 1)
      setPurchaseList([...purchase_list])
      handlePurchaseList([...purchase_list])
    }
  }, [purchase_list, handlePurchaseList])



  return (
    <React.Fragment>
      <AddProduct updateList={updateList}/>
      <div className="card overflow-hidden">
        <TableContainer>
          <Table size="small">
            <TableHead>
              <TableRow>
                <TableCell className="font-weight-bold">Name</TableCell>
                <TableCell className="font-weight-bold">Genric</TableCell>
                <TableCell className="font-weight-bold" align="center">Quantity</TableCell>
                <TableCell className="font-weight-bold" align="center">Unit Price</TableCell>
                <TableCell className="font-weight-bold" align="center">MRP</TableCell>
                <TableCell className="font-weight-bold" align="center">Total</TableCell>
                <TableCell className="font-weight-bold" width={80}>Action</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {purchase_list.map((product, i) => (
                <TableRow key={i} hover>
                  <TableCell component="th" scope="row">{product.product_name}</TableCell>
                  <TableCell>{product.category}</TableCell>
                  <TableCell align="center">{product.quantity}</TableCell>
                  <TableCell align="center">{product.unit_price}</TableCell>
                  <TableCell align="center">{product.mrp_price}</TableCell>
                  <TableCell align="center">{(product.unit_price * product.quantity).toLocaleString()}</TableCell>
                  <TableCell className="p-0">
                    <DeleteButton dispatch={dispatch} rowKeyValue={i}/>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </div>
    </React.Fragment>
  );
})
