import React from 'react';
import {TextField, Fab, InputAdornment} from '@material-ui/core';
import SearchRoundedIcon from '@material-ui/icons/SearchRounded';
import SupplierSearch from 'components/input/SupplierSearch';
import CustomerSearch from 'components/input/CustomerSearch';
import cogoToast from 'cogo-toast';


export default React.memo(({handleSearch, searching, his_type, ledger})=>{
  const date = new Date()
  const today = `${date.getFullYear()}-${('0'+(date.getMonth()+1)).slice(-2)}-${('0'+(date.getDate())).slice(-2)}`
  const [history_date, setDate] = React.useState({start_date: today, end_date: today})
  const [invoice, setInvoice] = React.useState('')
  const [errors, setErrors] = React.useState({supplier: false, invoice: false})

  const byDate = React.useCallback(()=>{
    if(ledger && !history_date.supplier_code){
      setErrors({supplier: true, invoice: false})
      cogoToast.warn('Please select a supplier first')
      return
    }
    setErrors({invoice: false, supplier: false})
    setInvoice('')
    handleSearch('date', history_date)
  }, [handleSearch, history_date, ledger])

  const byInvoice = React.useCallback(()=>{
    if(invoice){
      setErrors((prev)=>({...prev, invoice: false}))
      setDate('')
      handleSearch('invoice', invoice)
    } else setErrors((prev)=>({...prev, invoice: true}))
  }, [handleSearch, invoice])

  const handleDateChange = (e)=>{
    setDate({...history_date, [e.target.name]: e.target.value})
  }

  return(
    <div className="form-row">
      {!ledger &&
        <div className="col-md-3 col-sm-6">
          <div className="card p-2">
            <div className="form-row">
              <div className="col">
                <TextField
                  label="Search by Invoice"
                  placeholder="Invoice"
                  variant="outlined"
                  size="small"
                  fullWidth
                  autoFocus
                  required
                  error={errors.invoice}
                  helperText={errors.invoice && "Invoice is required"}
                  value={invoice}
                  onChange={(e)=>setInvoice(e.target.value)}
                  onKeyPress={(e)=>e.key==='Enter' && byInvoice()}
                  InputProps={{
                    startAdornment: <InputAdornment position="start">#</InputAdornment>,
                  }}
                />
              </div>
              <div className="col-md-3 col-sm-4 d-flex align-items-center justify-content-center">
                <Fab color="primary" size="small" onClick={byInvoice} disabled={searching}>
                  <SearchRoundedIcon/>
                </Fab>
              </div>
            </div>
          </div>
        </div>
      }
      <div className="col-md-9 col-sm-8">
        <div className="card p-2">
          <div className="form-row">
            {his_type === "PURCHASE" ?
              <div className="col">
                <SupplierSearch
                  label="Supplier"
                  name="supplier_code"
                  required={ledger}
                  error={errors.supplier}
                  handleInputs={handleDateChange}
                />
              </div> :
              <div className="col">
                <CustomerSearch
                  label="Customer"
                  name="customer_code"
                  handleInputs={handleDateChange}
                />
              </div>
            }
            <div className="col">
              <TextField
                label="From"
                name="start_date"
                type="date"
                variant="outlined"
                size="small"
                fullWidth
                defaultValue={history_date.start_date}
                onChange={handleDateChange}
                onKeyPress={(e)=>e.key==='Enter' && byDate()}
                InputLabelProps={{shrink: true}}
              />
            </div>
            <div className="col">
              <TextField
                label="To"
                name="end_date"
                type="date"
                variant="outlined"
                size="small"
                fullWidth
                defaultValue={history_date.end_date}
                onChange={handleDateChange}
                onKeyPress={(e)=>e.key==='Enter' && byDate()}
                InputLabelProps={{shrink: true}}
              />
            </div>
            <div className="col-md-1 d-flex align-items-center justify-content-center">
              <Fab color="primary" size="small" onClick={byDate} disabled={searching}>
                <SearchRoundedIcon/>
              </Fab>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
})