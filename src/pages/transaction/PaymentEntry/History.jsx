import React from 'react';
import {
  Collapse,
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow
} from '@material-ui/core';
import Pagination from '@material-ui/lab/Pagination';
import {
  KeyboardArrowDown,
  KeyboardArrowUp,
  ArrowBackRounded
} from '@material-ui/icons';
import DataLoading from 'components/loading/DataLoading';
import {PrintButton} from 'components/PDF';
import { transactionHistory } from '../server_action';
import {url} from 'util/Api';


const Row = React.memo((props)=>{
  const { row } = props;
  const [open, setOpen] = React.useState(false);
  const payment_date = new Date(row.payment_date.split('T')[0]).toDateString().split(' ').slice(1)

  return (
    <React.Fragment>
      <TableRow hover onClick={() => setOpen(!open)}>
        <TableCell className="pr-0">
          <IconButton size="small" onClick={() => setOpen(!open)}>
            {open ? <KeyboardArrowUp /> : <KeyboardArrowDown />}
          </IconButton>
        </TableCell>
        <TableCell>{`${payment_date[0]} ${payment_date[1]}, ${payment_date[2]}`}</TableCell>
        <TableCell className="font-weight-bold">
          {row.pay_for.map(val => val.payment_head).join(', ')}
        </TableCell>
        <TableCell>{row.payment_by}</TableCell>
        <TableCell align="right">{row.total_payment.toLocaleString()}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell className="p-0" colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <div className="mx-5 my-3 pb-4 border border-secondary shadow">
              <div className="d-flex justify-content-between align-items-center p-2">
                <h6 className="m-0 font-weight-bold text-secondary">Payment Details</h6>
                <PrintButton url={`${url}/print/paymentVoucher?voucher_id=${row.voucher_id}`} />
              </div>
              <Table size="small" aria-label="purchases">
                <TableHead>
                  <TableRow>
                    <TableCell className="text-primary border-primary font-weight-bold" width={150}>Date</TableCell>
                    <TableCell className="text-primary border-primary font-weight-bold">Payment for</TableCell>
                    <TableCell className="text-primary border-primary font-weight-bold">Description</TableCell>
                    <TableCell className="text-primary border-primary font-weight-bold" align="right">Amount</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {row.pay_for.map((pay) => (
                    <TableRow hover key={pay.payment_head}>
                      <TableCell className="border-primary" component="th" scope="row">
                        {`${payment_date[0]} ${payment_date[1]}, ${payment_date[2]}`}
                      </TableCell>
                      <TableCell className="border-primary">{pay.payment_head}</TableCell>
                      <TableCell className="border-primary">{pay.supplier_code||pay.description}</TableCell>
                      <TableCell className="border-primary" align="right">{pay.amount.toLocaleString()}</TableCell>
                    </TableRow>
                  ))}
                  
                  <TableRow>
                    <TableCell className="border-bottom-0" colSpan={2}/>
                    <TableCell className="border-bottom-0" align="right">Total</TableCell>
                    <TableCell className="border-bottom-0" align="right">{row.total_payment.toLocaleString()}</TableCell>
                  </TableRow>

                </TableBody>
              </Table>
            </div>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
})

// eslint-disable-next-line
export default ({history})=>{
  const [payment_list, setPaymentList] = React.useState([])
  const [loading, setLoading] = React.useState(true)
  const [page, setPage] = React.useState(0);
  const rowsPerPage = 10

  React.useEffect(()=>{
    transactionHistory('E')
      .then(resp => resp.success && setPaymentList(resp.list))
      .finally(()=> setLoading(false))
  },[])


  const handleChangePage = (_, newPage) => setPage(newPage-1);


  return (
    <div className="app-wrapper">
      <h5 className="text-center font-weight-bold mb-3">Payment History</h5>
      <div className="card overflow-hidden">
        <TableContainer>
          <Table size="small">
            <TableHead>
              <TableRow>
                <TableCell className="pr-0">
                  <IconButton size="small" onClick={()=>history.goBack()}>
                    <ArrowBackRounded color="secondary"/>
                  </IconButton>
                </TableCell>
                <TableCell className="text-primary font-weight-bold" style={{minWidth: 120}}>Payment Date</TableCell>
                <TableCell className="text-primary font-weight-bold">Paid For</TableCell>
                <TableCell className="text-primary font-weight-bold">Paid By</TableCell>
                <TableCell className="text-primary font-weight-bold" align="right">Total</TableCell>
              </TableRow>
            </TableHead>
              {!loading &&
                <TableBody>
                  {(rowsPerPage > 0
                    ? payment_list.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                    : payment_list).map((row, i) => (
                      <Row key={i} row={row} />
                    ))}
                </TableBody>
              }
          </Table>
          {loading 
            ? <DataLoading text="Getting history..."/>
            : <div className="d-flex justify-content-center mt-3 mb-2">
              <Pagination
                count={Math.ceil(payment_list.length/rowsPerPage)}
                page={page+1}
                onChange={handleChangePage}
                color="primary"
              />
            </div>
          }
        </TableContainer>
      </div>
    </div>
  );
}
