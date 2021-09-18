import React, { useEffect } from 'react';
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
import {willBeExpired} from 'pages/dashboard/server_action';



function ExpireDate({expire}) {
  const [open, setOpen] = React.useState(false);

  let date = new Date(expire.date.split('T')[0]).toDateString().split(' ')

  return (
    <React.Fragment>
      <TableRow hover onClick={()=>setOpen(!open)}>
        <TableCell className="border-bottom-0">
          <IconButton size="small" onClick={() => setOpen(!open)}>
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell className="border-bottom-0" component="th" scope="row">{date[2]}-{date[1]}-{date[3]}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell className="p-0" colSpan={2}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <div className="text-center py-2 mx-5 border">
              <strong>These products will be expired in - {date[1]} {date[2]}, {date[3]}</strong>
              <Table className="mt-2" size="small">
                <TableHead>
                  <TableRow>
                    <TableCell>Product Name</TableCell>
                    <TableCell>Batch</TableCell>
                    <TableCell align="right">Quantity</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {expire.product_list.map((product) => (
                    <TableRow key={product.product_code} hover>
                      <TableCell component="th" scope="row">{product.product_name}</TableCell>
                      <TableCell>{product.batch_no}</TableCell>
                      <TableCell align="right">{product.quantity}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </div>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}


export default function ExpiredWillBe() {
  const [will_expire, setWillExpire] = React.useState([])

  useEffect(()=>{
    willBeExpired()
      .then(resp => resp.success && setWillExpire(resp.message))
  }, [])

  return (
    <div className="card overflow-hidden">
      <strong className="text-center py-4 text-secondary">These items will be expired in 15 days</strong>
      <TableContainer>
        <Table size="small">
          <TableHead>
            <TableRow>
              <TableCell padding="checkbox"/>
              <TableCell className="font-weight-bold">Expire Date</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {will_expire.map((expire) => (
              <ExpireDate key={expire.date} expire={expire}/>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
}
