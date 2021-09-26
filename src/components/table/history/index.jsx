import React from 'react';
import Collapse from '@material-ui/core/Collapse';
import IconButton from '@material-ui/core/IconButton';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import KeyboardArrowDownIcon from '@material-ui/icons/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@material-ui/icons/KeyboardArrowUp';
import Details from './Details';
import Empty from 'components/empty';
import {supplierSearch} from 'pages/purchase/server_action';
import { PrintButton } from 'components/PDF';


function Product({history, history_type, ledger}) {
  const [open, setOpen] = React.useState(false);
  const date = new Date(history.entry_date).toDateString().split(' ')
  return (
    <React.Fragment>
      <TableRow hover onClick={() => setOpen(!open)}>
        <TableCell className="border-bottom-0 pl-2 pr-0">
          <IconButton size="small" onClick={() => setOpen(!open)}>
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell className="border-bottom-0" component="th" scope="row">
          {date[1]} {date[2]}, {date[3]}
        </TableCell>
        {!ledger &&
          <TableCell className="border-bottom-0" component="th" scope="row">
            {history_type==="Sales"?history.customer_name:history.supplier_name}
          </TableCell>
        }
        <TableCell className="border-bottom-0 font-weight-bold">{history.invoice}</TableCell>
        <TableCell className="border-bottom-0" align="right">{history.total.toLocaleString()}</TableCell>
        <TableCell className="border-bottom-0" align="right">{history.discount.toLocaleString()} ({history.discount_p}%)</TableCell>
        <TableCell className="border-bottom-0" align="right">{history.grand_total.toLocaleString()}</TableCell>
        <TableCell className="border-bottom-0" align="right">{history.payment.toLocaleString()}</TableCell>
        <TableCell className="border-bottom-0" align="right">{history.due.toLocaleString()}</TableCell>
      </TableRow>
      <TableRow style={{height: 0}}>
        <TableCell className="p-0" colSpan={9}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Details
              purchase_list={history_type==="Sales"?history.sales_list:history.purchase_list}
              history_type={history_type}
              history={history}
            />
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}

const SupplierInfo = ({info, search_data})=>(
  <div className="row p-3">
    <div className="col-10">
      <div className="form-row">
        <div className="col-md-4 col-sm-6 col-12">
          <div className="form-row">
            <div className="col-3">Name</div>
            <div className="col-9 animated fadeInRightTiny"><strong>:&nbsp;{info.name}</strong></div>
          </div>
          <div className="form-row">
            <div className="col-3">Company</div>
            <div className="col-9 animated fadeInRightTiny"><strong>:&nbsp;{info.company}</strong></div>
          </div>
          <div className="form-row">
            <div className="col-3">Phone</div>
            <div className="col-9 animated fadeInRightTiny"><strong>:&nbsp;{info.mobile}</strong></div>
          </div>
        </div>
        <div className="col-md-4 col-sm-6 col-12">
          <h6 className="text-center">Total Due</h6>
          <h4 className="text-center text-secondary animated slideInRightTiny">{info.due && info.due.toLocaleString()}</h4>
        </div>
      </div>
    </div>
    <div className="col-2 d-flex justify-content-end align-items-center">
      <PrintButton url={`/print/supplier/Ledger?supplier_code=${search_data.supplier_code}&start_date=${search_data.start_date}&end_date=${search_data.end_date}`}/>
    </div>
  </div>
)


export default function HistoryTable({history_list, history_type, ledger, search_data}) {
  const [supplier_info, setSupplierInfo] = React.useState({})

  React.useEffect(()=>{
    ledger && history_list.length>0 && supplierSearch(history_list[0].supplier_code)
      .then(resp => resp.success && setSupplierInfo(resp.message[0]))
  }, [ledger, history_list])

  if(history_list.length===0)
    return <Empty text="No report found!"/>
  
  // const print_url = history_type === 'Sales'
  //                     ? `/sale/history/print?customer_code=${search_data.customer_code}&start_date=${search_data.start_date}&end_date=${search_data.end_date}`
  //                     : `/purchase/history/print?supplier_code=${search_data.supplier_code}&start_date=${search_data.start_date}&end_date=${search_data.end_date}`

  return (
    <div className="card overflow-hidden">
      {ledger && <SupplierInfo info={supplier_info} search_data={search_data}/>}
      <TableContainer>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell className="border-primary p-0 text-center">
                {/* {!ledger && <PrintButton url={print_url}/>} */}
              </TableCell>
              <TableCell className="font-weight-bold border-primary">Date</TableCell>
              {!ledger &&
                <TableCell className="font-weight-bold border-primary">{history_type==='Sales'?'Customer':'Supplier'}</TableCell>
              }
              <TableCell className="font-weight-bold border-primary">Invoice</TableCell>
              <TableCell className="font-weight-bold border-primary" align="right">Total</TableCell>
              <TableCell className="font-weight-bold border-primary" align="right">Discount</TableCell>
              <TableCell className="font-weight-bold border-primary" align="right">Grand Total</TableCell>
              <TableCell className="font-weight-bold border-primary" align="right">Paid</TableCell>
              <TableCell className="font-weight-bold border-primary" align="right">Due</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {history_list.map((history) => (
              <Product key={history.transaction_id} history={history} history_type={history_type} ledger={ledger} />
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
}
