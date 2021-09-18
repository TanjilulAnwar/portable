import React from 'react'
import { Route, useHistory } from "react-router-dom";
import { useUserState } from "context/UserContext";
import AccessDenied from 'pages/error/AccessDenied';
import user_type from 'util/user_type';



const FilterRoute = ({component, role, ...rest})=>{
  var { userInfo } = useUserState();
  const history = useHistory()

  return (
    <Route
      {...rest}
      render={props =>
        role.some(val => val===userInfo.role) || userInfo.role === user_type.ADMIN
          ? ( React.createElement(component, props) )
          : ( <AccessDenied history={history}/> )
      }
    />
  );
}

export default FilterRoute;