import { toast } from "react-toastify";
import OrderTable from "../../components/orders/OrderTable";
import {
  useGetOrdersQuery,
  useUpdateOrderMutation,
} from "../../store/api/ordersApi";
import { useState } from "react";

function OrderManagement() {
  const { data: orders, isLoading, error, refetch } = useGetOrdersQuery();

  const [updateOrder] = useUpdateOrderMutation();

  const [showModal, setShowModal] = useState(false);
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [selectedOrder, setSelectedOrder] = useState(null);

  console.log(orders);

  const handleFormSubmit = async (formData) => {
    setIsSubmitting(true);
    try {
      let result;
      result = await createMenuItem(formDataToSend);
      if (result.isSuccess !== false) {
        toast.success("Menu item created successfully");
        refetch();
      } else {
        toast.error("Failed to create menu item");
      }

      setShowModal(false);
      resetForm();
    } catch (error) {
      console.log(error);
    } finally {
      setIsSubmitting(false);
    }
  };

  const handleEditOrder = (order) => {
    setShowModal(true);
    setSelectedOrder(order);
  };

  const handleCloseModal = () => {
    setShowModal(false);
    setSelectedOrder(null);
  };

  return (
    <div className="container-fluid p-4 mx-3">
      <div className="row mb-4">
        <div className="col">
          <div className="d-flex justify-content-between align-items-center">
            <div>
              <h2>Order Management</h2>
              <p className="text-muted mb-0">Manage your restaurant's order</p>
            </div>
          </div>
        </div>
      </div>
      <div className="row">
        <div className="col">
          <div className="card">
            <div className="card-body">
              <OrderTable
                orders={orders}
                isLoading={isLoading}
                error={error}
                onEdit={handleEditOrder}
              />
            </div>
          </div>
        </div>
      </div>
      {/* {showModal && (
        <MenuItemModal
          formData={formData}
          onSubmit={handleFormSubmit}
          onClose={handleCloseModal}
          isSubmitting={isSubmitting}
          onChange={handleInputChange}
          isEditing={!!selectedMenuItem}
        />
      )} */}
    </div>
  );
}

export default OrderManagement;
