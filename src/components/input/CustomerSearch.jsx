import React from 'react';
import {TextField, CircularProgress} from '@material-ui/core';
import Autocomplete from '@material-ui/lab/Autocomplete';
import {customerSearch} from 'pages/sales/server_action';


export default React.memo(({name, label, defaultValue, handleInputs, setCustomerInfo, required, error, margin, disabled})=>{
  const [Customer_list, setCustomerList] = React.useState([])
  const [loading, setLoading] = React.useState(false)

  React.useEffect(()=>{
    customerSearch('')
      .then(resp => resp.success && setCustomerList(resp.message))
  }, [])

  const handleSearch = React.useCallback((e)=>{
    setLoading(true)
    customerSearch(e.target.value)
      .then(resp => {
        if(resp.success)
          setCustomerList(resp.message)
        else setCustomerList([])
      })
      .finally(()=>setLoading(false))
  }, [])

  const handleSelected = React.useCallback((e, val)=>{
    setCustomerInfo && setCustomerInfo(val || {})
    let event = {target: {name, value: val?val.code:null}}
    handleInputs(event)
  }, [handleInputs, name, setCustomerInfo])

  return(
    <Autocomplete
      loadingText="Searching..."
      noOptionsText="No Customer found!"
      options={Customer_list}
      getOptionLabel={(option) => option.name}
      filterOptions={op=>op}
      defaultValue={defaultValue}
      onChange={handleSelected}
      loading={loading}
      disabled={disabled}
      renderInput={(params) => (
        <TextField
          {...params}
          label={label}
          placeholder="Search by Name/Mobile"
          variant="outlined"
          size="small"
          margin={margin}
          fullWidth
          disabled={disabled}
          required={required}
          error={error}
          onChange={handleSearch}
          InputProps={{
            ...params.InputProps,
            endAdornment: (
              <React.Fragment>
                {loading ? <CircularProgress color="secondary" size={20}/> : null}
                {params.InputProps.endAdornment}
              </React.Fragment>
            )
          }}
        />
      )}
    />
  )
})