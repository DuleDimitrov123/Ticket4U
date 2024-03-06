import { 
    Flex, 
    Heading, 
    Icon,
    TableContainer,
    Table,
    Tbody,
    Td,
    Th,
    Thead,
    Tr} from "@chakra-ui/react";
import styles from "./CategoryTable.styles";

const CategoriesTable = ({categories}) => {
    return (
        <TableContainer>
            <Table {...styles.table}>
                <Thead>
                <Tr>
                    <Th>Category name</Th>
                    <Th>Category description</Th>
                </Tr>
                </Thead>
                <Tbody>
                {categories?.map((category, index) => (
                    <Tr key={index}>
                    <Td>{category.name}</Td>
                    <Td>
                        {category.description}</Td>
                    </Tr>
                ))}
                </Tbody>
            </Table>
        </TableContainer>
    )
}
export default CategoriesTable;