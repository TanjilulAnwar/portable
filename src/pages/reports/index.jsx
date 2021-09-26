import React from 'react';
import {Route, Switch,  useRouteMatch} from 'react-router-dom';
// eslint-disable-next-line
import asyncComponent from '../../util/asyncComponent';


const ReportRoutes = React.memo(()=>{
  const path = useRouteMatch().path
  return(
    <div className="app-wrapper">
      <Switch>
      
        <Route path={`${path}/purchase-details`} component={asyncComponent(() => import('./PurchaseDetails'))}/>
        <Route path={`${path}/inventory-report`} component={asyncComponent(() => import('./InventoryReport'))}/>
        <Route path={`${path}/sales-details`} component={asyncComponent(() => import('./SalesDetails'))}/>
        <Route path={`${path}/stock-position`} component={asyncComponent(() => import('./StockPosition'))}/>
        <Route component={asyncComponent(() => import('../error/Error'))}/>
      </Switch>
    </div>
  )
})

export default ReportRoutes;