import React, { useState, useEffect } from "react";
import { Drawer, IconButton, List } from "@material-ui/core";
import {
  Home as HomeIcon,
  ArrowBack as ArrowBackIcon,
  Settings,
  Build,
  AddShoppingCart,
  LocalMallTwoTone,
  HistoryRounded,
  SwapVerticalCircleTwoTone,
  AssessmentTwoTone,
  AccountBalance,
  SyncRounded
} from "@material-ui/icons";
import { useTheme } from "@material-ui/styles";
import { withRouter } from "react-router-dom";
import classNames from "classnames";
import useStyles from "./styles";

// components
import SidebarLink from "./components/SidebarLink/SidebarLink";

// context
import {useLayoutState, useLayoutDispatch, toggleSidebar} from "../../context/LayoutContext";


const structure = [
  { label: "Dashboard", link: "/dashboard", icon: <HomeIcon />},
  { label: "Sales", link: "/sales", icon: <LocalMallTwoTone />},
  { label: "Purchase", link: "/purchase", icon: <AddShoppingCart />},
  { label: "History", link: "/history", icon: <HistoryRounded />,
    children: [
      { label: "Purchase", link: "/history/purchase" },
      { label: "Sales", link: "/history/sales" }
    ],
  },
  { label: "Return Product", link: "/return-product", icon: <SyncRounded />},
  { label: "Transaction", link: "/transaction", icon: <SwapVerticalCircleTwoTone/>,
    children: [
      { label: "Receipts Entry", link: "/transaction/receipts-entry" },
      { label: "Payment Entry", link: "/transaction/payment-entry" },
      { label: "Contra Entry", link: "/transaction/contra-entry" },
      { label: "Journal Entry", link: "/transaction/journal-entry" },
    ],
  },
  { label: "Stock Reports", link: "/reports", icon: <AssessmentTwoTone/>,
    children: [
      { label: "Purchase Details", link: "/reports/purchase-details" },
      { label: "Inventory Report", link: "/reports/inventory-report" },
      { label: "Sales Details", link: "/reports/sales-details" },
      { label: "Sales Purchase Summary", link: "/reports/summary-report" },
    ],
  },
  { label: "Product Configuration", link: "/product-configuration", icon: <Build />,
    children: [
      { label: "Manufacturers", link: "/product-configuration/manufacturers" },
      { label: "Generics", link: "/product-configuration/categories" },
      { label: "Products", link: "/product-configuration/products" },
    ],
  },
  { label: "Configuration", link: "/configuration", icon: <Settings />, 
    children: [
      // { label: "Trade Management", link: "/configuration/trade" },
      { label: "User Management", link: "/configuration/user" },
      { label: "Customers", link: "/configuration/customer" },
      { label: "Suppliers", link: "/configuration/supplier-entry"},
      { label: "Account Group", link: "/configuration/account-group"},
      { label: "Account Head", link: "/configuration/account-head"},
      { label: "Account Name", link: "/configuration/account-name"},
    ],
  },
  { label: "Accounts", link: "/account", icon: <AccountBalance />, 
    children: [
      { label: "Supplier Ledger", link: "/account/supplier-ledger" },
      { label: "Balance Sheet", link: "/account/balance-sheet" },
      { label: "Ledger Report", link: "/account/ledger-report" },
      { label: "Income Statement", link: "/account/income-statement" },
      { label: "Cashflow", link: "/account/cashflow" },
    ],
  },
];




const Sidebar = ({ location })=>{
  var classes = useStyles();
  var theme = useTheme();

  // global
  var { isSidebarOpened } = useLayoutState();
  var layoutDispatch = useLayoutDispatch();

  // local
  var [isPermanent, setPermanent] = useState(true);


  const handleWindowWidthChange = ()=>{
    var windowWidth = window.innerWidth;
    var breakpointWidth = theme.breakpoints.values.md;
    var isSmallScreen = windowWidth < breakpointWidth;

    if (isSmallScreen && isPermanent) {
      setPermanent(false);
    } else if (!isSmallScreen && !isPermanent) {
      setPermanent(true);
    }
  }

  useEffect(function() {
    window.addEventListener("resize", handleWindowWidthChange);
    handleWindowWidthChange();
    return function cleanup() {
      window.removeEventListener("resize", handleWindowWidthChange);
    };
  });

  return (
    <Drawer
      variant={isPermanent ? "permanent" : "temporary"}
      className={classNames(classes.drawer, {
        [classes.drawerOpen]: isSidebarOpened,
        [classes.drawerClose]: !isSidebarOpened,
      })}
      classes={{
        paper: classNames({
          [classes.drawerOpen]: isSidebarOpened,
          [classes.drawerClose]: !isSidebarOpened,
        }),
      }}
      open={isSidebarOpened}
    >
      <div className={classes.toolbar} />
      <div className={classes.mobileBackButton}>
        <IconButton onClick={() => toggleSidebar(layoutDispatch)}>
          <ArrowBackIcon
            classes={{
              root: classNames(classes.headerIcon, classes.headerIconCollapse),
            }}
          />
        </IconButton>
      </div>
      <List>
        {structure.map((link, i) => (
          <SidebarLink
            key={i}
            location={location}
            isSidebarOpened={isSidebarOpened}
            {...link}
          />
        ))}
      </List>
    </Drawer>
  );
}

export default withRouter(Sidebar);
