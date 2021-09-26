import React from 'react';
import {Route, withRouter, Switch, Redirect} from 'react-router-dom';
import asyncComponent from '../../util/asyncComponent';


const AccountsRoutes = (props)=>{
  const match = props.match.path

  return(
    <Switch>
      <Redirect exact from={match} to={`${match}/balance-sheet`} />
      <Route path={`${match}/ledger-report`} component={asyncComponent(() => import('./Ledger'))}/>
      <Route path={`${match}/cashflow`} component={asyncComponent(() => import('./Cashflow'))}/>
      <Route component={asyncComponent(() => import('../error/Error'))}/>
    </Switch>
  )
}

export default withRouter(AccountsRoutes);