import React from 'react';
import {TextField, CircularProgress} from '@material-ui/core';
import Autocomplete from '@material-ui/lab/Autocomplete';
import {supplierSearch} from 'pages/purchase/server_action';


export default React.memo(({name, label, defaultValue, handleInputs, setSupplierInfo, required, error, margin, disabled})=>{
  const [supplier_list, setSupplierList] = React.useState([])
  const [loading, setLoading] = React.useState(false)

  React.useEffect(()=>{
    supplierSearch('')
      .then(resp => {
        resp.success
          ? setSupplierList(resp.message)
          : setSupplierList([])
      })
  }, [])

  const handleSearch = React.useCallback((e)=>{
    setLoading(true)
    supplierSearch(e.target.value)
      .then(resp => {
        if(resp.success)
          setSupplierList(resp.message)
        else setSupplierList([])
      })
      .finally(()=>setLoading(false))
  }, [])

  const handleSelected = React.useCallback((e, val)=>{
    setSupplierInfo && setSupplierInfo(val || {})
    let event = {target: {name, value: val?val.code:null}}
    handleInputs(event)
  }, [handleInputs, name, setSupplierInfo])

  return(
    <Autocomplete
      loadingText="Searching..."
      noOptionsText="No supplier found!"
      options={supplier_list}
      getOptionLabel={(option) => option.name}
      autoHighlight={true}
      value={defaultValue}
      onChange={handleSelected}
      filterOptions={op=>op}
      disabled={disabled}
      loading={loading}
      renderInput={(params) => (
        <TextField
          {...params}
          label={label}
          placeholder="Search by Name/Mobile"
          variant="outlined"
          size="small"
          margin={margin}
          fullWidth
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