import React from 'react'
import FromToForm from 'components/forms/common/FromTo';


function Cashflow() {
  return (
    <div className="app-wrapper">
      <h5 className="text-center font-weight-bold">Cashflow</h5>
      <FromToForm
				// allow_no_start
				print_url="/print/cashflow"
			/>
    </div>
  )
}

export default Cashflow;
