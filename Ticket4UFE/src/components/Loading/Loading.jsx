import { Flex, Spinner } from '@chakra-ui/react';
import styles from './Loading.styles';

const Loading = () => (
  <Flex {...styles.wrapper}>
    <Spinner {...styles.loader} />
  </Flex>
);

export default Loading;
