import React from 'react';
import {
  FormControlLabel,
  Checkbox,
  Dialog,
  DialogTitle,
  DialogContent,
  Button,
} from '@material-ui/core';
import PersonAddIcon from '@material-ui/icons/PersonAdd';
import CheckCircleIcon from '@material-ui/icons/CheckCircle';
import CheckCircleOutlineIcon from '@material-ui/icons/CheckCircleOutline';
import CustomerForm from 'components/forms/configuration/customer';
import CustomerSearch from 'components/input/CustomerSearch';



const AddNewCustomer = React.memo(({open, handleClose, updateList})=>{
  return(
    <Dialog
      open={open}
      onClose={handleClose}
      aria-labelledby="dialog-title"
      aria-describedby="dialog-description"
      scroll="body"
    >
      <DialogTitle className="text-center" id="dialog-title">Add Customer</DialogTitle>
      <DialogContent>
        <CustomerForm updateList={updateList} update={false} handleClose={handleClose}/>
      </DialogContent>
    </Dialog>
  )
})


const CustomerInfo = ({form_inputs, handleInputs})=>{
  const [open, setOpen] = React.useState(false)
  const [customer_info, setCustomerInfo] = React.useState(null)

  const handleClose = ()=> setOpen(false)

  React.useEffect(()=>{
    setCustomerInfo(null)
  }, [])

  const getCustomerInfo = (info)=>{
    handleClose()
    setCustomerInfo(info)
  }

  
  return (
    <div className="card px-3 py-2">
      <div className="form-row">
        <div className="col-md-2 d-flex align-items-center">
          <FormControlLabel
            className="m-0"
            checked={form_inputs.g_customer}
            onClick={handleInputs}
            control={<Checkbox icon={<CheckCircleOutlineIcon />} checkedIcon={<CheckCircleIcon />} name="g_customer" color="primary" className="m-0" />}
            label="General Customer"
          />
        </div>
        <div className="col-md-4">
          <CustomerSearch                                    // Customer search
            label="Search Customer"
            name="customer_code"
            margin="dense"
            defaultValue={customer_info}
            disabled={form_inputs.g_customer}
            setCustomerInfo={setCustomerInfo}
            handleInputs={handleInputs}
          />
        </div>

        {customer_info && !form_inputs.g_customer &&
          <div className="col-xl-3 col-md-4 col sm-5">
            <div className="d-flex flex-column align-items-center justify-content-center">
              <span><strong>Customer Name : </strong>&nbsp;{customer_info.name}</span>
              <hr className="w-50 p-0 m-0"/>
              <span><strong>Mobile : </strong>&nbsp;{customer_info.mobile}</span>
            </div>
          </div>
        } 

        <div className="col d-flex align-items-center justify-content-end">
          <Button color="primary" variant="outlined" size="small" onClick={()=>setOpen(true)} disabled={form_inputs.g_customer}>
            <PersonAddIcon fontSize="small" /> &nbsp; Add Customer
          </Button>
        </div>

      </div>
      <AddNewCustomer open={open} handleClose={handleClose} updateList={getCustomerInfo}/>
    </div>
  );
}


export default React.memo(CustomerInfo);