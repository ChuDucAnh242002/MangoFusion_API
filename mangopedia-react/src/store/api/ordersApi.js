import { baseApi } from "./baseApi";

export const ordersApi = baseApi.injectEndpoints({
    endpoints: (builder) => ({
        // Create all endpoints
        getOrders: builder.query({
            query: (userId = "") => ({
                url: "/OrderHeader",
                params: userId ? { userId } : {}
            }),
            providesTags: ["Order"],
            transformResponse: (response) => {
                if (response && response.result && Array.isArray(response.result)) {
                    return response.result;
                }
                if (response && Array.isArray(response)) {
                    return response.result;
                }
                return [];
            },
        }),

        getOrderById: builder.query({
            query: (id) => `/OrderHeader/${id}`,
            providesTags: (result, error, { id }) => [{ type: "Order", id }],
            transformResponse: (response) => {
                if (response && response.result) {
                    return response.result;
                }
                return response;
            },
        }),
        createOrder: builder.mutation({
            query: (formData) => ({
                url: "/OrderHeader",
                method: "POST",
                body: formData,
            }),
            invalidatesTags: ["Order"],
        }),
        updateOrder: builder.mutation({
            query: ({ id, formData }) => ({
                url: `/OrderHeader?id=${id}`,
                method: "PUT",
                body: formData,
            }),
            invalidatesTags: (result, error, { id }) => [{ type: "Order", id }],
        }),
    }),
});

export const { useGetOrdersMutation, useCreateOrderMutation, useUpdateOrderMutation, useGetOrderByIdMutation } = ordersApi;
