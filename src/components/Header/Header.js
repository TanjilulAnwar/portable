import React, { useState } from "react";
import {useHistory} from 'react-router-dom';
import {
  AppBar,
  Avatar,
  Toolbar,
  IconButton,
  Menu,
  MenuItem,
  Button
} from "@material-ui/core";
import {
  Menu as MenuIcon,
  Person as AccountIcon,
  MenuOpen,
  VpnKey,
  AccountCircle,
  KeyboardArrowDownRounded
} from "@material-ui/icons";
import classNames from "classnames";

// styles
import useStyles from "./styles";

// components
import { useLayoutState, useLayoutDispatch, toggleSidebar } from "context/LayoutContext";
import { useUserDispatch, signOut, useUserState, setTradeCode } from "context/UserContext";
import user_type from 'util/user_type';
import { url } from 'util/Api';
import { red } from "@material-ui/core/colors";


const TradeManu = ({trade_list})=>{
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [selectedIndex, setSelectedIndex] = React.useState(0);
  const history = useHistory();
  
  const handleMenuItemClick = (_, index) => {
    setSelectedIndex(index);
    setAnchorEl(null);
    setTradeCode(trade_list[index].code)
    history.replace(history.location.pathname)
  };

  return (
    <div>
      <Button size="small" color="secondary" onClick={(event)=>setAnchorEl(event.currentTarget)}>
        <span>{'{ '}{trade_list[selectedIndex].name} <KeyboardArrowDownRounded fontSize="small"/>{'}'}</span>
      </Button>
      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={()=>setAnchorEl(null)}
      >
        {trade_list.map((trade, index) => (
          <MenuItem
            key={trade.id}
            selected={index === selectedIndex}
            onClick={(event) => handleMenuItemClick(event, index)}
          >
            {trade.name}
          </MenuItem>
        ))}
      </Menu>
    </div>
  );
}

export default function Header(props) {
  var classes = useStyles();
  var layoutState = useLayoutState();
  var layoutDispatch = useLayoutDispatch();
  var userDispatch = useUserDispatch();
  var { userInfo } = useUserState();
  var [profileMenu, setProfileMenu] = useState(null);
  const history = useHistory();
  const user_role = Object.keys(user_type).find(val=>user_type[val]===userInfo.role)

  const handleResetPassword = ()=>{
    setProfileMenu(null)
    history.push('/reset-password')
  }

  return (
    <AppBar position="fixed" className={classes.appBar} >
      <Toolbar className={classes.toolbar}>
        {userInfo.role!==user_type.SYSADMIN &&
          <IconButton
            color="inherit"
            onClick={() => toggleSidebar(layoutDispatch)}
            className={classNames(classes.headerMenuButtonSandwich, classes.headerMenuButtonCollapse)}
          >
            {layoutState.isSidebarOpened ? (
              <MenuOpen classes={{root: classNames(classes.headerIcon, classes.headerIconCollapse)}}/>
            ) : (
              <MenuIcon classes={{root: classNames(classes.headerIcon, classes.headerIconCollapse)}}/>
            )}
          </IconButton>
        }

        {userInfo.role!==user_type.SYSADMIN &&
          <Avatar src={`${url}${userInfo.client && userInfo.client.logo}`} variant="rounded" alt="" />
        }
        <h4 className={classes.logotype}>
          {userInfo.role===user_type.SYSADMIN?"System Admin panel":userInfo.client.name}
        </h4>
        {/* {userInfo.trade && userInfo.trade.length !== 0
          ? <TradeManu trade_list={userInfo.trade}/>
          : <Button color="secondary" variant="outlined" size="small" onClick={()=>history.push('/configuration/trade')} disabled={userInfo.role===user_type.SYSADMIN}>
              Create Trade
            </Button>
        } */}
        <div className={classes.grow}/>

        <IconButton
          aria-haspopup="true"
          color="inherit"
          className={classes.headerMenuButton}
          onClick={e => setProfileMenu(e.currentTarget)}
        >
          <AccountCircle classes={{ root: classes.headerIcon }} />
        </IconButton>
        <Menu
          id="profile-menu"
          open={Boolean(profileMenu)}
          anchorEl={profileMenu}
          onClose={() => setProfileMenu(null)}
          className={classes.headerMenu}
          classes={{ paper: classes.profileMenu }}
          disableAutoFocusItem
        >
 
          <div className={classes.profileMenuUser}>
            <h4 variant="h4" weight="medium">{userInfo.name}</h4>
            <MenuItem style={{color: "black"}} className={classNames(classes.profileMenuItem, classes.headerMenuItem)} onClick={handleResetPassword}>
            <VpnKey className={classes.profileMenuIcon} /> Change password
          </MenuItem>
            <strong>{user_role}</strong>
            <span className={classes.profileMenuLink} color="primary">
              {userInfo.phone}
            </span>
          </div>
          {/* <MenuItem className={classNames(classes.profileMenuItem, classes.headerMenuItem)} disabled>
            <AccountIcon className={classes.profileMenuIcon} /> Profile
          </MenuItem> */}
         
          <div className={`${classes.profileMenuUser} text-center`}>
            <strong
              className={`${classes.profileMenuLink} border border-secondary rounded`}
              color="primary"
              onClick={() => signOut(userDispatch, props.history)}
            >
              Sign Out
            </strong>
          </div>
        </Menu>
      </Toolbar>
    </AppBar>
  );
}
