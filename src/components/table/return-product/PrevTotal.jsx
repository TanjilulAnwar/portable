import React from 'react';

const PrevTotal = ({info})=>{
  const d = new Date(info.entry_date.split('T')[0]).toDateString().split(' ')
  return(
    <div className="card p-3">
      <h6 className="text-center text-primary">Previous Information</h6>
      <hr className="w-50 m-auto"/>
      <div className="row mt-3">
        <div className="col text-right">Date :</div>
        <div className="col"><strong>{d[2]} {d[1]}, {d[3]}</strong></div>
      </div>
      <div className="row">
        <div className="col text-right">Net Total :</div>
        <div className="col">{info.total.toLocaleString()}</div>
      </div>
      <div className="row">
        <div className="col text-right">Discount :</div>
        <div className="col">{info.discount.toLocaleString()} ({info.discount_p}%)</div>
      </div>
      <div className="row">
        <div className="col text-right">Grand Total :</div>
        <div className="col"><strong>{info.grand_total.toLocaleString()}</strong></div>
      </div>
    </div>
  )
}

export default PrevTotal;