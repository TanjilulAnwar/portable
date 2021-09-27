import React from 'react';
import {Route, withRouter, Switch, Redirect} from 'react-router-dom';
import asyncComponent from '../../util/asyncComponent';
import FilterRoute from 'components/FilterRoutes';
import user_type from 'util/user_type';


const {SALES, INVENTORY, ACCOUNTS} = user_type;

const ConfigurationRoutes = (props)=>{
  const match = props.match.path

  return(
    <div className="app-wrapper">
      <Switch>
        <Redirect exact from={match} to={`${match}/supplier-entry`} />
        <FilterRoute role={[]} path={`${match}/user`} component={asyncComponent(() => import('./UserManagement'))}/>
        <FilterRoute role={[SALES]} path={`${match}/customer`} component={asyncComponent(() => import('./Customers'))}/>
        <FilterRoute role={[INVENTORY]} path={`${match}/supplier-entry`} component={asyncComponent(() => import('./SupplierEntry'))}/>
        <FilterRoute role={[ACCOUNTS]} path={`${match}/account-group`} component={asyncComponent(() => import('./AccountGroup'))}/>
        <FilterRoute role={[ACCOUNTS]} path={`${match}/account-head`} component={asyncComponent(() => import('./AccountHead'))}/>
        <FilterRoute role={[ACCOUNTS]} path={`${match}/account-name`} component={asyncComponent(() => import('./AccountName'))}/>
        <Route component={asyncComponent(() => import('../error/Error'))}/>
      </Switch>
    </div>
  )
}

export default withRouter(ConfigurationRoutes);