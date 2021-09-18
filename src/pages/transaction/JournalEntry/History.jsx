import React from 'react';
import {
  IconButton,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  TextField,
  Fab,
  Collapse
} from '@material-ui/core';
import Pagination from '@material-ui/lab/Pagination';
import {
  KeyboardArrowDown,
  KeyboardArrowUp,
  ArrowBackRounded,
  SearchRounded
} from '@material-ui/icons';
import Empty from 'components/empty';
import DataLoading from 'components/loading/DataLoading';
import { journalMonth } from '../server_action';


const Row = React.memo((props)=>{
  const { row } = props;
  const [open, setOpen] = React.useState(false);
  const entry_date = new Date(row.journal_date.split('T')[0]).toDateString().split(' ').slice(1)

  let total_dabit = 0
  let total_credit = 0
  row.journal_debit.forEach(val => total_dabit += val.amount)
  row.journal_credit.forEach(val => total_credit += val.amount)

  return (
    <React.Fragment>
      <TableRow hover onClick={() => setOpen(!open)}>
        <TableCell className="py-2">
          <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
            {open ? <KeyboardArrowUp /> : <KeyboardArrowDown />}
          </IconButton>
        </TableCell>
        <TableCell className="py-2">{`${entry_date[0]} ${entry_date[1]}, ${entry_date[2]}`}</TableCell>
        <TableCell className="py-2">{row.reference}</TableCell>
        <TableCell className="py-2">{row.journal_debit.map(val=>val.ac_head_name).join(', ')}</TableCell>
        <TableCell className="py-2">{row.journal_credit.map(val=>val.ac_head_name).join(', ')}</TableCell>
        <TableCell className="py-2" align="right">{total_dabit.toLocaleString()}</TableCell>
      </TableRow>
      <TableRow>
        <TableCell className="p-0" colSpan={6}>
          <Collapse in={open} timeout="auto" unmountOnExit className="d-flex justify-content-center">
            <div className="my-3 pb-4 border border-secondary shadow">
              <div className="text-center p-2">
                <h6 className="m-0 font-weight-bold text-secondary">Journal Report For - {`${entry_date[0]} ${entry_date[1]}, ${entry_date[2]}`}</h6>
                <span>{row.description}</span>
              </div>
              <Table size="small">
                <TableHead>
                  <TableRow>
                    <TableCell className="text-primary border-primary font-weight-bold">Account Name</TableCell>
                    <TableCell className="text-primary border-primary font-weight-bold" align="right">Debit</TableCell>
                    <TableCell className="text-primary border-primary font-weight-bold" align="right">Credit</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {row.journal_debit.map((debit) => (
                    <TableRow hover key={debit.ac_head_id}>
                      <TableCell className="border-primary">{debit.ac_head_name}</TableCell>
                      <TableCell className="border-primary" align="right">{debit.amount.toLocaleString()}</TableCell>
                      <TableCell className="border-primary"/>
                    </TableRow>
                  ))}
                  {row.journal_credit.map((credit) => (
                    <TableRow hover key={credit.ac_head_id}>
                      <TableCell className="border-primary"> &nbsp; - &nbsp; {credit.ac_head_name}</TableCell>
                      <TableCell className="border-primary"/>
                      <TableCell className="border-primary" align="right">{credit.amount.toLocaleString()}</TableCell>
                    </TableRow>
                  ))}
                  
                  <TableRow>
                    <TableCell className="border-bottom-0 font-weight-bold" align="right">Total</TableCell>
                    <TableCell className="border-bottom-0 font-weight-bold" align="right">
                      <ins>{total_dabit.toLocaleString()}</ins>
                    </TableCell>
                    <TableCell className="border-bottom-0 font-weight-bold" align="right">
                      <ins>{total_credit.toLocaleString()}</ins>
                    </TableCell>
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
  const [history_list, setHistoryList] = React.useState([])
  const [loading, setLoading] = React.useState(true)
  const [page, setPage] = React.useState(0);
  const rowsPerPage = 10


  const emptyRows = rowsPerPage - Math.min(rowsPerPage, history_list.length - page * rowsPerPage);

  const handleChangePage = (_, newPage) => setPage(newPage-1);

  return (
    <div className="app-wrapper">
      <h5 className="text-center font-weight-bold mb-3">Journal History</h5>
      <DateField setHistoryList={setHistoryList} loading={loading} setLoading={setLoading}/>
      {history_list.length === 0
        ? <Empty text="No History found!"/>
        : <div className="card overflow-hidden">
            <TableContainer>
              <Table aria-label="collapsible table">
                <TableHead style={{background: 'var(--light)'}}>
                  <TableRow>
                    <TableCell className="py-0 pr-0" width={50}>
                      <IconButton className="p-2" onClick={()=>history.goBack()}>
                        <ArrowBackRounded fontSize="small" color="secondary"/>
                      </IconButton>
                    </TableCell>
                    <TableCell className="text-primary font-weight-bold" width={130}>Journal Date</TableCell>
                    <TableCell className="text-primary font-weight-bold" width={120}>Reference</TableCell>
                    <TableCell className="text-primary font-weight-bold">Debit Name</TableCell>
                    <TableCell className="text-primary font-weight-bold">Credit Name</TableCell>
                    <TableCell className="text-primary font-weight-bold" align="right">Amount</TableCell>
                  </TableRow>
                </TableHead>
                {!loading &&
                  <TableBody>
                    {(rowsPerPage > 0
                      ? history_list.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                      : history_list).map((row, i) => (
                        <Row key={i} row={row} />
                      ))}

                    {emptyRows > 0 && (
                      <TableRow><TableCell colSpan={6}/></TableRow>
                    )}
                  </TableBody>
                }
              </Table>
              {loading 
                ? <DataLoading text="Getting history..."/>
                : <div className="d-flex justify-content-center mt-3 mb-2">
                    <Pagination
                      count={Math.ceil(history_list.length/rowsPerPage)}
                      page={page+1}
                      onChange={handleChangePage}
                      color="primary"
                    />
                  </div>
              }
            </TableContainer>
          </div>
      }
    </div>
  );
}


const DateField = ({setHistoryList, loading, setLoading})=>{
  const date = new Date()
  const today = `${date.getFullYear()}-${('0'+(date.getMonth()+1)).slice(-2)}`
  const [history_month, setMonth] = React.useState(today)

  const handleShow = ()=>{
    setLoading(true)
    journalMonth(history_month)
      .then(resp => resp.success ? setHistoryList(resp.message):setHistoryList([]))
      .finally(()=>setLoading(false))
  }

  React.useEffect(()=>{
    handleShow()
  // eslint-disable-next-line
  }, [history_month])

  return(
    <div className="d-flex justify-content-center">
      <div className="card py-2 px-3">
        <div className="form-row">
          <div className="col">
            <TextField
              label="Journal Month"
              name="history_month"
              type="month"
              variant="outlined"
              size="small"
              fullWidth
              defaultValue={history_month}
              onChange={(e)=>setMonth(e.target.value)}
              onKeyPress={(e)=>e.key==='Enter' && setMonth(e.target.value)}
              InputLabelProps={{shrink: true}}
            />
          </div>
          <div className="col-md-3 col-sm-3 d-flex align-items-center justify-content-center">
            <Fab color="primary" size="small" disabled={loading} onClick={handleShow}>
              <SearchRounded/>
            </Fab>
          </div>
        </div>
      </div>
    </div>
  )
}
