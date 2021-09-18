import React from 'react';
import {
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow
} from '@material-ui/core';
import Pagination from '@material-ui/lab/Pagination';
import { ArrowBackRounded } from '@material-ui/icons';
import DataLoading from 'components/loading/DataLoading';
import { contraHistory } from '../server_action';


const Row = React.memo((props)=>{
  const { row } = props;
  const entry_date = new Date(row.entry_date.split('T')[0]).toDateString().split(' ').slice(1)

  return (
    <React.Fragment>
      <TableRow hover>
        <TableCell/>
        <TableCell className="py-2">{`${entry_date[0]} ${entry_date[1]}, ${entry_date[2]}`}</TableCell>
        <TableCell className="py-2">{row.ac_head_name_cr}</TableCell>
        <TableCell className="py-2">{row.ac_head_name_dr}</TableCell>
        <TableCell className="py-2">{row.reference}</TableCell>
        <TableCell className="py-2" align="right">{row.amount.toLocaleString()}</TableCell>
      </TableRow>
    </React.Fragment>
  );
})

// eslint-disable-next-line
export default ({history})=>{
  const [contra_list, setContraList] = React.useState([])
  const [loading, setLoading] = React.useState(true)
  const [page, setPage] = React.useState(0);
  const rowsPerPage = 10

  React.useEffect(()=>{
    contraHistory()
      .then(resp => resp.success && setContraList(resp.message))
      .finally(()=> setLoading(false))
  },[])

  const emptyRows = rowsPerPage - Math.min(rowsPerPage, contra_list.length - page * rowsPerPage);

  const handleChangePage = (_, newPage) => setPage(newPage-1);

  return (
    <div className="app-wrapper">
      <h5 className="text-center font-weight-bold mb-3">Contra History</h5>
      <div className="card overflow-hidden">
        <TableContainer>
          <Table aria-label="collapsible table">
            <TableHead style={{background: 'var(--light)'}}>
              <TableRow>
                <TableCell className="py-0 pr-0" width={50}>
                  <IconButton className="p-2" onClick={()=>history.goBack()}>
                    <ArrowBackRounded fontSize="small" color="secondary"/>
                  </IconButton>
                </TableCell>
                <TableCell className="text-primary font-weight-bold" style={{minWidth: 120}}>Transfer Date</TableCell>
                <TableCell className="text-primary font-weight-bold">Transfer From</TableCell>
                <TableCell className="text-primary font-weight-bold">Transfer To</TableCell>
                <TableCell className="text-primary font-weight-bold">Reference</TableCell>
                <TableCell className="text-primary font-weight-bold" align="right">Amount</TableCell>
              </TableRow>
            </TableHead>
            {!loading &&
              <TableBody>
                {(rowsPerPage > 0
                  ? contra_list.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  : contra_list).map((row, i) => (
                    <Row key={i} row={row} />
                  ))}

                {emptyRows > 0 && (
                  <TableRow><TableCell colSpan={5}/></TableRow>
                )}
              </TableBody>
            }
          </Table>
          {loading 
            ? <DataLoading text="Getting history..."/>
            : <div className="d-flex justify-content-center mt-3 mb-2">
              <Pagination
                count={Math.ceil(contra_list.length/rowsPerPage)}
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
