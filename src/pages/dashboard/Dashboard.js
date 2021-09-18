import React from "react";
import WillExpired from "components/table/dashboard/WillExpired";
import ExpiredProducts from 'components/table/dashboard/Expired';


class Dashboard extends React.Component{
  render(){
    return(
      <div className="app-wrapper">
        <h5 className="text-center mb-3 font-weight-bold">Dashboard</h5>
        <div className="form-row">
          <div className="col-md-6">
            <WillExpired/>
          </div>
          <div className="col-md-6">
            <ExpiredProducts/>
          </div>
        </div>
      </div>
    )
  }
}

export default Dashboard;