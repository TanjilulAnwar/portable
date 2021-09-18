import React from 'react';
import {
  FormControl,
  InputLabel,
  OutlinedInput,
  InputAdornment,
  IconButton
} from '@material-ui/core';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPercent, faDollarSign } from '@fortawesome/free-solid-svg-icons'


const DiscountField = ({percent, handleInputs, handleDiscountType})=>{
  return(
    <FormControl variant="outlined" size="small" margin="dense">
      <InputLabel htmlFor="discount">Discount</InputLabel>
      <OutlinedInput
        id="discount"
        name="discount"
        type="number"
        onChange={handleInputs}
        endAdornment={
          <InputAdornment position="end">
            <IconButton
              onClick={handleDiscountType}
              edge="end"
              color="secondary"
            >
              {percent ? <FontAwesomeIcon className="_heartbeat" icon={faPercent}/> : <FontAwesomeIcon className="_heartbeat" icon={faDollarSign}/>}
            </IconButton>
          </InputAdornment>
        }
        labelWidth={70}
      />
    </FormControl>
  )
}

export default DiscountField;