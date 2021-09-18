import React from 'react';
import PurchaseDetailsForm from 'components/forms/reports/PurchaseDetails';

const PurchaseDetails = ()=>{
  return(
    <div>
      <h5 className="text-center font-weight-bold mb-3">Purchase Details</h5>
      <PurchaseDetailsForm print_url="/print/DetailReportPurchase"/>
    </div>
  )
}

export default PurchaseDetails