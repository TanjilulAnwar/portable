import React from 'react';
import { Route, Switch, withRouter } from 'react-router-dom';
import asyncComponent from 'util/asyncComponent';
import JournalForm from 'components/forms/transaction/journal-entry';


const JournalEntry = (props)=>{
  return(
    <div className="app-wrapper">
      <h5 className="text-center text-primary font-weight-bold mb-3">Journal Entry</h5>
      <JournalForm {...props}/>
    </div>
  )
}

const JournalEntryRoutes = ({match, history}) =>
	<Switch>
		<Route path={`${match.url}/j-history`} component={asyncComponent(() => import("./History"))}/>
		<Route path={`${match.url}`} exact component={()=><JournalEntry match={match} history={history}/>}/>
		<Route component={asyncComponent(() => import("pages/error/Error"))}/>
	</Switch>

export default withRouter(JournalEntryRoutes);
