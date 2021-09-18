import React from 'react'
import FromToForm from 'components/forms/common/FromTo';


export default React.memo(()=>{
	return(
		<div className="app-wrapper">
			<h5 className="text-center font-weight-bold">Income Statement</h5>
			<FromToForm
				allow_no_start
				print_url="/print/IncomeStatement"
			/>
		</div>
	)
})