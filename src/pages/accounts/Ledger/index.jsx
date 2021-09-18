import React from 'react';
import FromToForm from 'components/forms/common/FromTo';


const LedgerReport = ()=>{
  return(
    <div className="app-wrapper">
      <h5 className="text-center font-weight-bold">Ledger Report</h5>
      <FromToForm print_url="/print/Ledger"/>
    </div>
  )
}

export default React.memo(LedgerReport);