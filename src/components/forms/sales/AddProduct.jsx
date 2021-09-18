import React from 'react';
import {Button, TextField} from '@material-ui/core';
import { useForm } from "react-hook-form";
import SearchProduct from 'components/input/ProductSearch';


const AddProduct = ({addToTable})=>{
  const { register, handleSubmit, reset, formState: { errors, isValid } } = useForm({
    mode: 'onTouched',
    reValidateMode: 'onChange'
  });
  const [pro_error, setProError] = React.useState(false)
  const [product, setProduct] = React.useState({})
  
  
  const handleAdd = (data, e)=>{
    if(!!product.product_code && isValid){
      const new_data = {...product, ...data}
      reset()
      addToTable(new_data)
      setProduct({})
      setProError(false)
    } else setProError(true)
  }

  return(
    <div className="d-flex justify-content-center">
      <div className="card p-2">
        <form onSubmit={handleSubmit(handleAdd)}>
          <div className="d-flex justify-content-center">
            <SearchProduct
              name="product"
              autoFocus={true}
              error={pro_error}
              defaultValue={product}
              setProductInfo={setProduct}
            />
            {Object.keys(product).length>0 && (
              <React.Fragment>
                <div className="ml-2 text-center border rounded px-1">
                  <span className="text-muted">Available</span><br/>
                  <strong>{product.quantity}</strong>
                </div>
                <div className="mr-2 text-center border rounded px-1">
                  <span className="text-muted">MRP</span><br/>
                  <strong>{product.mrp_price}</strong>
                </div>
              </React.Fragment>
            )}
            <TextField
              label="Quantity"
              placeholder="Quantity"
              variant="outlined"
              size="small"
              type="number"
              required
              style={{maxWidth: 120}}
              error={!!errors.quantity}
              inputProps={{...register("quantity", {required: true}), required: ''}}
            />
            <TextField
              label="Discount"
              placeholder="Discount"
              variant="outlined"
              size="small"
              type="number"
              style={{maxWidth: 120}}
              inputProps={{...register("discount")}}
            />
            <div className="d-flex align-items-center ml-2">
              <Button type="submit" color="secondary" variant="contained" size="small">
                Add
              </Button>
            </div>
          </div>
        </form>
      </div>
    </div>
  )
}

export default AddProduct;