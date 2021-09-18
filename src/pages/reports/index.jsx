import React from 'react';
import {Route, Switch, Redirect, useRouteMatch} from 'react-router-dom';
// eslint-disable-next-line
import asyncComponent from '../../util/asyncComponent';


const ReportRoutes = React.memo(()=>{
  const path = useRouteMatch().path
  return(
    <div className="app-wrapper">
      <Switch>
        <Redirect exact from={path} to={`${path}/summary-report`} />
        <Route path={`${path}/purchase-details`} component={asyncComponent(() => import('./PurchaseDetails'))}/>
        <Route path={`${path}/inventory-report`} component={asyncComponent(() => import('./InventoryReport'))}/>
        <Route path={`${path}/sales-details`} component={asyncComponent(() => import('./SalesDetails'))}/>
        <Route path={`${path}/summary-report`} component={asyncComponent(() => import('./SummaryReport'))}/>
        <Route component={asyncComponent(() => import('../error/Error'))}/>
      </Switch>
    </div>
  )
})

export default ReportRoutes;