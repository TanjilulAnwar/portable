import React from 'react';
import ReturnForm from 'components/forms/return-product';
import ProductTable from 'components/table/return-product';
import DataLoading from 'components/loading/DataLoading';


class ReturnProduct extends React.Component{
  state = {
    loading: false,
    product_info: {}
  }

  setLoading = (loading)=>{this.setState({loading: loading})}

  setProductInfo = (info)=>{
    this.setState({product_info: info})
  }

  render(){
    return(
      <div className="app-wrapper">
        <h5 className="text-center font-weight-bold mb-3">Return Product</h5>
        <ReturnForm
          loading={this.state.loading}
          setLoading={this.setLoading.bind(this)}
          setProductInfo={this.setProductInfo.bind(this)}
        />
        {!this.state.loading
          ? !!this.state.product_info.invoice &&
            <ProductTable product_info={this.state.product_info}/>
          : <DataLoading text="Getting Data..." />
        }
      </div>
    )
  }
}

export default ReturnProduct;