export default async function getProductByCode(code: string) {
    try {
        const result = await window.api.product.getCode(code.toUpperCase());
        if (!result) {
            return null;
        }
        return result;

    } catch (error) {
        console.log('Error catch api/product----->' + error);
    }
}