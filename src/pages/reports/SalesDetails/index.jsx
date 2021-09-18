import React from 'react';
import FromToForm from 'components/forms/common/FromTo';

const SalesDetails = ()=>{
  return(
    <div>
      <h5 className="text-center font-weight-bold mb-3">Sales Details</h5>
      <FromToForm print_url="/print/DetailReportSales"/>
    </div>
  )
}

export default SalesDetails