import React from 'react';
import {Button, TextField} from '@material-ui/core';
import SearchProduct from 'components/input/ProductSearch';
import { useForm } from "react-hook-form";


const AddProductForm = ({updateList})=>{
  const { register, handleSubmit, setValue, reset, formState: { errors } } = useForm({
    mode: 'onTouched',
    reValidateMode: 'onChange'
  });
  const [pro_error, setProError] = React.useState(false)
  const [product, setProduct] = React.useState({})

  const selectedProduct = (pro_info)=>{
    setValue('mrp_price', pro_info ? pro_info.mrp_price : 0)
    setProduct(pro_info)
  }

  const addToList = (data)=>{
    if(Object.keys(product).length>0){
      setProError(false)
      data['quantity'] = parseInt(data['quantity'])
      data['mrp_price'] = parseFloat(data['mrp_price'])
      data['unit_price'] = parseFloat(data['unit_price'])
      const new_data = {...product, ...data}
      updateList(new_data)
      setProduct({})
      reset()
    } else setProError(true)
  }


  return(
    <div className="d-flex justify-content-center">
      <div className="card p-2">
        <form onSubmit={handleSubmit(addToList)}>
          <div className="d-flex">
            <SearchProduct
              name="product"
              autoFocus={true}
              error={pro_error}
              defaultValue={product}
              setProductInfo={selectedProduct}
            />
            {Object.keys(product).length>0 &&
              <div className="mx-1 text-center border rounded px-1">
                <span className="text-muted">Available</span><br/>
                <strong>{product.quantity}</strong>
              </div>
            }
            <TextField
              label="Quantity"
              placeholder="Quantity"
              variant="outlined"
              size="small"
              type="number"
              required
              style={{maxWidth: 100}}
              error={!!errors.quantity}
              inputProps={{...register("quantity", {required: true, min: 0}), required: ''}}
            />
            <TextField
              label="Unit Price"
              placeholder="Unit Price"
              variant="outlined"
              size="small"
              type="number"
              required
              style={{maxWidth: 110}}
              error={!!errors.unit_price}
              inputProps={{...register("unit_price", {required: true, min: 0}), step: 0.01, required: ''}}
            />
            <TextField
              label="MRP"
              placeholder="MRP"
              variant="outlined"
              size="small"
              type="number"
              required
              style={{maxWidth: 120}}
              error={!!errors.mrp_price}
              inputProps={{...register("mrp_price", {required: true, min: 0}), step: 0.01, required: ''}}
              InputLabelProps={{shrink: true}}
            />
            <TextField                                     // Batch number
              label="Batch No"
              placeholder="Batch Number"
              name="batch_no"
              variant="outlined"
              size='small'
              style={{minWidth: 120}}
              inputProps={{...register("batch_no")}}
            />
            <TextField                                     // expire date
              label="Expire Date"
              name="expire_date"
              type="date"
              variant="outlined"
              size='small'
              required
              style={{minWidth: 165}}
              inputProps={{...register("expire_date", {required: true})}}
              InputLabelProps={{shrink: true}}
            />
            <div className="d-flex align-items-center ml-2">
              <Button color="secondary" type="submit" variant="contained" size="small">
                Add
              </Button>
            </div>
          </div>
        </form>
      </div>
    </div>
  )
}

export default React.memo(AddProductForm);