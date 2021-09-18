import React from 'react';
import FromToForm from 'components/forms/common/FromTo';

const SummaryReport = ()=>{
  return(
    <div>
      <h5 className="text-center font-weight-bold mb-3">Sales-Purchase Summary</h5>
      <FromToForm print_url="/print/SalesPurchaseSummary"/>
    </div>
  )
}

export default SummaryReport