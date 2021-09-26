import React from "react";
import {
  Route,
  Switch,
  Redirect,
  withRouter,
} from "react-router-dom";
import classnames from "classnames";
import useStyles from "./styles";
import Header from "../Header";
import Sidebar from "../Sidebar";
import Configuration from "../../pages/configuration";
import Accounts from "../../pages/accounts";
import ProductConfiguration from "../../pages/product-configuration";
import History from "../../pages/history";
import Transaction from "../../pages/transaction";
import ReportRoutes from "../../pages/reports";
import { useLayoutState } from "../../context/LayoutContext";
import asyncComponent from '../../util/asyncComponent';
import FilterRoute from 'components/FilterRoutes';
import user_type from 'util/user_type';


const {SALES, INVENTORY, ACCOUNTS} = user_type;

const Routes = React.memo(({path})=>{
  return(
    <Switch>
      <Redirect exact from={path} to="/dashboard" />
      <Route path="/dashboard" component={asyncComponent(() => import('pages/dashboard'))} />
      <FilterRoute role={[SALES]} path="/sales" component={asyncComponent(() => import('pages/sales'))} />
      <FilterRoute role={[INVENTORY]} path="/purchase" component={asyncComponent(() => import('pages/purchase'))} />
      <Route path="/history" component={History}/>
      {/* <FilterRoute role={[SALES]} path="/return-product" component={asyncComponent(() => import('pages/ReturnProduct'))}/> */}
      <FilterRoute role={[ACCOUNTS]} path="/transaction" component={Transaction}/>
      <FilterRoute role={[ACCOUNTS]} path="/reports" component={ReportRoutes}/>
      <FilterRoute role={[INVENTORY]} path="/product-configuration" component={ProductConfiguration}/>
      <Route path="/configuration" component={Configuration}/>
      <FilterRoute role={[ACCOUNTS]} path="/account" component={Accounts}/>
      <Route path="/reset-password" component={asyncComponent(() => import('pages/ChangePassword'))}/>
      <Route component={asyncComponent(() => import('../../pages/error/Error'))} />
    </Switch>
  )
})

function Layout(props) {
  var classes = useStyles();
  var layoutState = useLayoutState();
  const match = props.match
  
  return (
    <div className={classes.root}>
      <React.Fragment>
        <Header history={props.history} />
        <Sidebar />
        <div className={classnames(classes.content, {
            [classes.contentShift]: layoutState.isSidebarOpened,
          })}
        >
          <Routes path={match.path} reset={props.location.state?.reset}/>
        </div>
      </React.Fragment>
    </div>
  );
}

export default withRouter(Layout);
