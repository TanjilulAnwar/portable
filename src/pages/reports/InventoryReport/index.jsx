import React from 'react';
import FromToForm from 'components/forms/common/FromTo';

const InventoryReport = ()=>{
  return(
    <div>
      <h5 className="text-center font-weight-bold mb-3">Inventory Report</h5>
      <FromToForm print_url="/print/Inventory"/>
    </div>
  )
}

export default InventoryReport