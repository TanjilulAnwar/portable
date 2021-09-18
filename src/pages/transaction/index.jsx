import React from 'react';
import {Route, Switch, Redirect, useRouteMatch} from 'react-router-dom';
import asyncComponent from '../../util/asyncComponent';


const ReceiptsEntry = asyncComponent(() => import('./ReceiptsEntry'));
const PaymentEntry = asyncComponent(() => import('./PaymentEntry'));
const ContraEntry = asyncComponent(() => import('./ContraEntry'));
const JournalEntry = asyncComponent(() => import('./JournalEntry'));

const TransactionRoutes = React.memo(()=>{
  const path = useRouteMatch().path

  return(
    <div className="app-wrapper">
      <Switch>
        <Redirect exact from={path} to={`${path}/receipts-entry`} />
        <Route path={`${path}/receipts-entry`} component={ReceiptsEntry}/>
        <Route path={`${path}/payment-entry`} component={PaymentEntry}/>
        <Route path={`${path}/contra-entry`} component={ContraEntry}/>
        <Route path={`${path}/journal-entry`} component={JournalEntry}/>
        <Route component={asyncComponent(() => import('../error/Error'))}/>
      </Switch>
    </div>
  )
})

export default TransactionRoutes;