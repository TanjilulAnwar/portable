import React from 'react'
import HistoryForm from 'components/forms/history'
import HistoryTable from 'components/table/history'
import { searchByDate } from 'pages/history/server_action'
import DataLoading from 'components/loading/DataLoading'


function SupplierLedger() {
  const [status, setStatus] = React.useState({loading: false})
  const [list, setList] = React.useState(null)
  const search_data = React.useRef({})
  
  const handleSearch = (_, data)=>{
    setStatus({loading: true})
    search_data.current = data
    searchByDate(data)
      .then(resp => setList(resp.success?resp.message:[]))
      .finally(()=> setStatus({loading: false}))
  }

  
  return (
    <div className="app-wrapper">
      <h5 className="text-center font-weight-bold">Supplier Ledger</h5>
      <HistoryForm handleSearch={handleSearch} searching={status.loading} his_type="PURCHASE" ledger={true}/>
      {status.loading && <DataLoading/>}
      {!status.loading && list && 
        <HistoryTable history_list={list} history_type="Purchase" ledger={true} search_data={search_data.current}/>
      }
    </div>
  )
}


export default React.memo(SupplierLedger);