import React from 'react';
import { PrintButton } from 'components/PDF';
const SummaryReport = ()=>{
  return(
    <div>
      <h5 className="text-center font-weight-bold mb-3">Stock Position</h5>



      <div className="card p-2">
      <div className="form-row">

      <div className="col-md-3"><p style={{fontWeight:600,padding:10}}>
      Todays available stock: </p>
        </div>
        <div className="col-md-3" style={{fontWeight:600,padding:10}}>
        <PrintButton url="/print/StockPosition"/>
        </div>

      </div></div>

   
    </div>
  )
}

export default SummaryReport