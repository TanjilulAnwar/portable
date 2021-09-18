import React from "react";
import { Button } from "@material-ui/core";
import { ArrowBack } from '@material-ui/icons';
import logo from "images/svg/404.svg";

export default function AccessDenied({history}) {
  return (
    <div className="app-wrapper" style={{height: '95%'}}>
      <div className="d-flex flex-column justify-content-center align-items-center h-100">
        <img width="20%" src={logo} alt="Access Denied" />
        <h1 className="text-primary font-weight-bold _heartbeat">Page Not Found!</h1>
        <h5 className="text-muted my-3">
          Sorry, The page you are looking for is not exist.
        </h5>
        <Button
          color="primary"
          variant="contained"
          className="w-25"
          size="small"
          onClick={()=>history.goBack()}
        >
          <ArrowBack fontSize="small"/> &nbsp; Go Back
        </Button>
      </div>
    </div>
  );
}
