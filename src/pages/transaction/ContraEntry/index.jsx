import React from 'react';
import { Route, Switch, withRouter } from 'react-router-dom';
import asyncComponent from 'util/asyncComponent';
import ContraForm from 'components/forms/transaction/contra-entry';


const ContraEntry = (props)=>{
  return(
    <div className="app-wrapper">
      <h5 className="text-center font-weight-bold mb-3">Contra Entry</h5>
      <ContraForm {...props}/>
    </div>
  )
}

const ContraEntryRoutes = ({match}) =>
	<Switch>
		<Route path={`${match.url}/c-history`} component={asyncComponent(() => import("./History"))}/>
		<Route path={`${match.url}`} exact component={()=><ContraEntry match={match}/>}/>
		<Route component={asyncComponent(() => import("pages/error/Error"))}/>
	</Switch>

export default withRouter(ContraEntryRoutes);
