import React from 'react';
import {TextField, CircularProgress} from '@material-ui/core';
import Autocomplete from '@material-ui/lab/Autocomplete';
import {searchProduct} from 'pages/sales/server_action';


export default React.memo((props)=>{
  const {name, label, defaultValue, handleInputs, setProductInfo, required, error, margin, autoFocus} = props

  const [product_list, setProductList] = React.useState([])
  const [loading, setLoading] = React.useState(false)
  const searchRef = React.useRef(null)

  React.useEffect(()=>{
    autoFocus && searchRef.current && searchRef.current.focus()
    searchProduct(defaultValue?defaultValue.product_name:'')
      .then(resp => {
        resp.success
          ? setProductList(resp.message)
          : setProductList([])
      })
  }, [defaultValue, autoFocus])

  const handleSearch = React.useCallback((e)=>{
    setLoading(true)
    searchProduct(e.target.value)
      .then(resp => {
        if(resp.success)
          setProductList(resp.message)
        else setProductList([])
      })
      .finally(()=>setLoading(false))
  }, [])

  const handleSelected = React.useCallback((e, val)=>{
    setProductInfo && setProductInfo(val || {})
    let event = {target: {name, value: val?val.product_code:null}}
    handleInputs && handleInputs(event)
  }, [handleInputs, name, setProductInfo])


  
  return(
    <Autocomplete
      style={{ minWidth: 330 }}
      loadingText="Searching..."
      noOptionsText="No Product found!"
      options={product_list}
      getOptionLabel={(option) => option.product_name}
      filterOptions={op=>op}
      autoHighlight={true}
      value={defaultValue}
      onChange={handleSelected}
      loading={loading}
      renderInput={(params) => (
        <TextField
          {...params}
          label={label}
          placeholder="Search product by Name/Code"
          variant="outlined"
          size="small"
          onChange={handleSearch}
          margin={margin}
          autoFocus={autoFocus}
          fullWidth
          required={required}
          error={error}
          inputRef={searchRef}
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