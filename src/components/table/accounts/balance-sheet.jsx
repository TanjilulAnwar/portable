import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';


export default function BalanceSheet({balance, date}) {
  const d = date.split('-')
  return (
    <div className="card p-0">
      <h5 className="text-center font-weight-bold pt-3">Balance Sheet as of - {d[2]}/{d[1]}/{d[0]}</h5>
      <TableContainer>
        <Table size="small" aria-label="spanning table">
          <TableHead>
            <TableRow>
              <TableCell className="border-primary" align="left">Account Ledger</TableCell>
              <TableCell className="border-primary" align="right" width="140">Debit Amount</TableCell>
              <TableCell className="border-primary" align="right" width="140">Credit Amount</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            <TableRow>
              <TableCell className="text-secondary" colSpan={3}><strong>Asset</strong></TableCell>
            </TableRow>
            {balance.asset && balance.asset.map((row) => (
              <TableRow key={row.account}>
                <TableCell>{row.account}</TableCell>
                <TableCell align="right">{row.amount}</TableCell>
                <TableCell align="right"/>
              </TableRow>
            ))}
            <TableRow>
              <TableCell className="text-secondary" colSpan={3}><strong>Liabilities</strong></TableCell>
            </TableRow>
            {balance.liability && balance.liability.map((row) => (
              <TableRow key={row.account}>
                <TableCell>{row.account}</TableCell>
                <TableCell align="right"/>
                <TableCell align="right">{row.amount}</TableCell>
              </TableRow>
            ))}
            <TableRow>
              <TableCell className="text-secondary" colSpan={3}><strong>Owners Equity</strong></TableCell>
            </TableRow>
            {balance.ownersEquity && balance.ownersEquity.map((row) => (
              <TableRow key={row.account}>
                <TableCell>{row.account}</TableCell>
                <TableCell align="right"/>
                <TableCell align="right">{row.amount}</TableCell>
              </TableRow>
            ))}

            <TableRow>
              <TableCell className="border-0 font-weight-bold" align="right">Total</TableCell>
              <TableCell className="border-0 font-weight-bold" align="right"><ins>{balance.totalDebit}</ins></TableCell>
              <TableCell className="border-0 font-weight-bold" align="right"><ins>{balance.totalCredit}</ins></TableCell>
            </TableRow>
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
}
