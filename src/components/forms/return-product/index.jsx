import React from 'react';
import {
  TextField,
  RadioGroup,
  Radio,
  FormControlLabel,
  FormControl,
  InputAdornment,
  Fab
} from '@material-ui/core';
import {SearchRounded} from '@material-ui/icons';
import {searchByInvoice} from 'pages/history/server_action';
import cogoToast from 'cogo-toast';


const ReturnForm = ({loading, setLoading, setProductInfo})=>{
  const [flag, setFlag] = React.useState("S");
  const [invoice, setInvoice] = React.useState('')
  const [invoiceErr, setinvoiceErr] = React.useState(false)
  const [supplier, setSupplier] = React.useState({})

  const getProductList = ()=>{
    if(invoice){
      setLoading(true)
      setinvoiceErr(false)
      setProductInfo({})
      searchByInvoice(invoice, flag==='C')
        .then(resp => {
          if(resp.success){
            setProductInfo(resp.message.length>0 ? resp.message[0] : {})
            setSupplier(resp.message && resp.message[0])
          } else cogoToast.warn(resp.message)
        })
        .finally(()=> setLoading(false))
    } else setinvoiceErr(true)
  }

  return(
    <div className="card p-2">
      <div className="form-row">
        <div className="col-md-3">
          <FormControl component="fieldset">
            <RadioGroup row name="flag" value={flag} onChange={(e)=>setFlag(e.target.value)}>
              <FormControlLabel value="S" control={<Radio />} label="Supplier" />
              <FormControlLabel value="C" control={<Radio />} label="Customer" />
            </RadioGroup>
          </FormControl>
        </div>
        <div className="col-md-3 d-flex align-items-center">
          <TextField
            label="Invoice"
            placeholder="Invoice"
            fullWidth
            variant="outlined"
            size="small"
            required
            autoFocus
            error={invoiceErr}
            helperText={invoiceErr && "Invoice is required"}
            value={invoice}
            onChange={(e)=>setInvoice(e.target.value)}
            onKeyPress={(e)=>e.key==='Enter' && getProductList()}
            InputProps={{
              startAdornment: <InputAdornment position="start">#</InputAdornment>,
            }}
          />
        </div>
        <div className="col-md-2 d-flex align-items-center">
          <Fab className="ml-3" color="primary" size="small" onClick={getProductList} disabled={loading}>
            <SearchRounded/>
          </Fab>
        </div>
        {(supplier.supplier_name || supplier.customer_name) &&
          <div className="col d-flex align-items-center">
            <div>Name: {supplier.supplier_name || supplier.customer_name}</div>
          </div>
        }
      </div>
    </div>
  )
}

export default React.memo(ReturnForm);