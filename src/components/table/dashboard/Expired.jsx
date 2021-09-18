import React from 'react';
import { lighten, makeStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Toolbar from '@material-ui/core/Toolbar';
import Checkbox from '@material-ui/core/Checkbox';
import IconButton from '@material-ui/core/IconButton';
import Tooltip from '@material-ui/core/Tooltip';
import DeleteIcon from '@material-ui/icons/Delete';
import Pagination from '@material-ui/lab/Pagination';
import {expired} from 'pages/dashboard/server_action';


function EnhancedTableHead(props) {
  const { onSelectAllClick, numSelected, rowCount } = props;
  return (
    <TableHead>
      <TableRow>
        <TableCell padding="checkbox">
          <Checkbox
            indeterminate={numSelected > 0 && numSelected < rowCount}
            checked={rowCount > 0 && numSelected === rowCount}
            onChange={onSelectAllClick}
          />
        </TableCell>
        <TableCell className="font-weight-bold" padding="none">Name</TableCell>
        <TableCell className="font-weight-bold">Code</TableCell>
        <TableCell className="font-weight-bold">Batch No.</TableCell>
        <TableCell className="font-weight-bold">Expired Date</TableCell>
      </TableRow>
    </TableHead>
  );
}

const useToolbarStyles = makeStyles((theme) => ({
  highlight: {
    color: theme.palette.secondary.main,
    backgroundColor: lighten(theme.palette.secondary.light, 0.85),
  },
  title: {flex: '1 1 100%'},
}));

const EnhancedTableToolbar = (props) => {
  const classes = useToolbarStyles();
  const { numSelected } = props;

  return (
    <Toolbar className={numSelected > 0 ? classes.highlight : ''}>
      {numSelected > 0 
        ? <strong className={classes.title}>{numSelected} selected</strong>
        : <strong className={`${classes.title} text-center text-secondary`}>Expired Products</strong>
      }

      {numSelected > 0 && (
        <Tooltip title="Delete">
          <IconButton aria-label="delete">
            <DeleteIcon />
          </IconButton>
        </Tooltip>
      )}
    </Toolbar>
  );
};

export default function ExpiredProducts() {
  const [product_list, setProductList] = React.useState([])
  const [selected, setSelected] = React.useState([]);
  const [page, setPage] = React.useState(0);
  const rowsPerPage = 10;

  React.useEffect(()=>{
    expired()
      .then(resp => resp.success && setProductList(resp.message))
  },[])

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelecteds = product_list.map((n) => n.product_code);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleClick = (_, name) => {
    const selectedIndex = selected.indexOf(name);
    let newSelected = [];

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, name);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1),
      );
    }

    setSelected(newSelected);
  };

  const handleChangePage = (_, newPage) => setPage(newPage-1)
  const isSelected = (name) => selected.indexOf(name) !== -1;


  return (
    <div className="card overflow-hidden">
      <EnhancedTableToolbar numSelected={selected.length} />
      <TableContainer>
        <Table size="small">
          <EnhancedTableHead
            numSelected={selected.length}
            onSelectAllClick={handleSelectAllClick}
            rowCount={product_list.length}
          />
          <TableBody>
            {product_list.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              .map((product, index) => {
                const isItemSelected = isSelected(product.product_code);
                const labelId = `enhanced-table-checkbox-${index}`;

                return (
                  <TableRow hover
                    onClick={(event) => handleClick(event, product.product_code)}
                    role="checkbox"
                    aria-checked={isItemSelected}
                    tabIndex={-1}
                    key={product.product_code}
                    selected={isItemSelected}
                  >
                    <TableCell padding="checkbox">
                      <Checkbox color="primary" checked={isItemSelected}/>
                    </TableCell>
                    <TableCell component="th" id={labelId} scope="row" padding="none">
                      {product.product_name}
                    </TableCell>
                    <TableCell>{product.product_code}</TableCell>
                    <TableCell>{product.batch_no}</TableCell>
                    <TableCell>{product.expire_date.split('T')[0]}</TableCell>
                  </TableRow>
                );
              })}
          </TableBody>
        </Table>
      </TableContainer>
      <Pagination
        count={Math.ceil(product_list.length/rowsPerPage)}
        onChange={handleChangePage}
        variant="outlined"
        color="primary"
        className="py-2 m-auto"
      />
    </div>
  );
}
