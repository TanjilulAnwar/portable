import React from 'react';
import SupplierForm from 'components/forms/purchase/Supplier';
import AmountForm from 'components/forms/purchase/Amount';
import ProductTable from './ProductTable';
import { AnimateOnChange } from 'react-animation';
import checkValidation from 'util/checkValidation';
import cogoToast from 'cogo-toast';
import {purchaseConfirm} from './server_action';


const Total = React.memo(({total})=>{
  return(
    <div className="text-center">
      <h5 className="text-primary font-italic">Total Price</h5>
      <h2 className="text-monospace text-secondary">
        <AnimateOnChange animationIn="bounceIn" animationOut="bounceOut" durationOut={300}>
          {total?total.toLocaleString():0}
        </AnimateOnChange>  
      </h2>
    </div>
  )
})


const calculate_grand_total = (percent, total, discount)=>{
  let grand_total = 0
  if(discount)
    grand_total = percent ? (total-(total*discount/100)) : (total-discount)
  else grand_total = total
  return parseFloat(parseFloat(grand_total).toFixed(2))
}

class Purchase extends React.Component{
  constructor(props){
    super(props)
    const date = new Date()
    const today = `${date.getFullYear()}-${('0'+(date.getMonth()+1)).slice(-2)}-${('0'+(date.getDate())).slice(-2)}`
    this.state={
      form_inputs: {entry_date: today, account_head_id: '01020000', percent: true},
      supplier_confirmed: false,
      require_field: {supplier_code: false},
      purchase_list: [],
      total: 0,
      saving: false,
      saved: false,
      invoice: ''
    }
  }

  handleSuplierConfirmed = ()=> this.setState({supplier_confirmed: true})

  handleFormInputs = (e)=>{
    const prev_state = this.state.form_inputs
    prev_state[e.target.name] = e.target.value
    if(e.target.name === 'discount' || e.target.name === "percent"){
      let discount = e.target.name === 'discount' ? parseFloat(e.target.value||0) : prev_state.discount || 0
      let t = calculate_grand_total(prev_state.percent, this.state.total, discount)
      prev_state['discount'] = discount
      prev_state['grand_total'] = t<0?0:t
      prev_state['payment'] = prev_state['grand_total']
    } else if(e.target.name === 'payment')
      prev_state['payment'] = parseFloat(e.target.value || 0)
    this.setState({form_inputs: {...prev_state}})
  }

  handlePurchaseList = (list)=>{
    let total = 0
    list.forEach(val=>{
      total += val.unit_price * val.quantity
    })
    let grand_total = calculate_grand_total(this.state.form_inputs.percent, total, this.state.form_inputs.discount)
    grand_total = grand_total<0?0:grand_total
    this.setState({purchase_list: list, total, form_inputs: {...this.state.form_inputs, payment: grand_total, grand_total}})
  }

  handleSave = ()=>{
    const {isFormValid, require_fields} = checkValidation(this.state.form_inputs, this.state.require_field)
    if(isFormValid){
      if(this.state.purchase_list.length > 0){
        this.setState({saving: true})
        const {hide} = cogoToast.loading('Saving information...')
        const output = this.state.form_inputs
        output['purchase_list'] = this.state.purchase_list
        purchaseConfirm(output)
          .then(resp => {
            if(resp.success){
              cogoToast.success('Product added successfully!')
              this.setState({saved: true, invoice: resp.invoice})
            } else cogoToast.error(resp.message)
          })
          .finally(()=>{this.setState({saving: false}); hide()})
      } else cogoToast.error('Please, Add some product')
    } else cogoToast.error('Please, fill up all required fields!')
    this.setState({require_field: {...require_fields}})
  }

  render(){
    return(
      <div className="app-wrapper">
        <h5 className="text-center font-weight-bold mb-3">Purchase Product</h5>
        <div className={this.state.supplier_confirmed ? "_disabled-block" : undefined}>
          <SupplierForm
            form_inputs={this.state.form_inputs}
            handleInputs={this.handleFormInputs.bind(this)}
            require_fields={this.state.require_field}
            confirmed={this.state.supplier_confirmed}
            handleSuplierConfirmed={this.handleSuplierConfirmed}
          />
        </div>
        {this.state.supplier_confirmed &&
          <div>
            <ProductTable handlePurchaseList={this.handlePurchaseList.bind(this)}/>
            <Total total={this.state.total}/>
            <AmountForm
              form_inputs={this.state.form_inputs}
              handleInputs={this.handleFormInputs.bind(this)}
              handleSave={this.handleSave.bind(this)}
              saving={this.state.saving}
              saved={this.state.saved}
              invoice={this.state.invoice}
            />
          </div>
        }
      </div>
    )
  }
}

export default Purchase;