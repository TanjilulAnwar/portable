import React from 'react';
import {
  IconButton,
  Button,
  Checkbox
} from '@material-ui/core';
import { deleteRow, selectAllRows, deselectRow, deselectAllRows, selectRow, selectRowsRange } from 'ka-table/actionCreators';
import DataRowContent from "ka-table/Components/DataRowContent/DataRowContent";
import {
  EditTwoTone,
  DeleteTwoTone,
  AddRounded
} from '@material-ui/icons';


export const DataRow = React.memo(
  props => <DataRowContent {...props} />,
  () => true
);

export const ActionButton = React.memo((props) => {
  const { dispatch, rowKeyValue, rowData, del, disabled } = props
  return (
    <div className="d-flex justify-content-center">
      <IconButton className="p-1" onClick={() => dispatch({type: 'EDIT', rowData})} disabled={disabled}>
        <EditTwoTone color="primary"/>
      </IconButton>
      {del &&
        <IconButton className="p-1" onClick={() => dispatch(deleteRow(rowKeyValue))}>
          <DeleteTwoTone color="error"/>
        </IconButton>
      }
    </div>
  );
})


export const AddButton = React.memo((props) => {
  const { dispatch } = props
  return (
    <div className="d-flex justify-content-center">
      <Button className="p-0 m-0" variant="contained" color="secondary" onClick={() => dispatch({type: 'ADD'})}>
        <AddRounded fontSize="small"/> Add
      </Button>
    </div>
  );
})


export const DeleteButton = React.memo((props) => {
  const { dispatch, rowKeyValue } = props
  return (
    <div className="d-flex justify-content-center">
      <IconButton className="p-1" onClick={() => dispatch(deleteRow(rowKeyValue))}>
        <DeleteTwoTone color="error"/>
      </IconButton>
    </div>
  );
})


export const SelectRow = ({rowKeyValue, dispatch, isSelectedRow, selectedRows}) => {
  return (
    <Checkbox
      color="primary"
      className="m-0 p-0"
      checked={isSelectedRow}
      onChange={(event) => {
        if (event.nativeEvent.shiftKey){
          dispatch(selectRowsRange(rowKeyValue, [...selectedRows].pop()));
        } else if (event.currentTarget.checked) {
          dispatch(selectRow(rowKeyValue));
        } else {
          dispatch(deselectRow(rowKeyValue));
        }
      }}
    />
  );
};


export const SelectAll = ({dispatch, areAllRowsSelected}) => {
  return (
    <Checkbox
      className="m-0 p-0"
      checked={areAllRowsSelected}
      onChange={(event) => {
        if (event.currentTarget.checked) {
          dispatch(selectAllRows()); // also available: selectAllVisibleRows(), selectAllRows()
        } else {
          dispatch(deselectAllRows()); // also available: deselectAllVisibleRows(), deselectAllRows()
        }
      }}
    />
  );
};