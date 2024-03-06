import { useParams } from "react-router-dom";
import useShows from "../../hooks/useShows";
import usePerformers from "../../hooks/usePerformers";
import AuthenticatedLayout from "../../layout/AuthenticatedLayout";
import {
  Box,
  Center,
  Divider,
  Flex,
  Heading,
  Icon,
  Image,
  Tab,
  TabIndicator,
  TabList,
  TabPanel,
  TabPanels,
  Tabs,
  Text,
  useDisclosure,
} from "@chakra-ui/react";
import useCategories from "../../hooks/useCategories";
import { BiLoader } from "react-icons/bi";
import AboutShow from "../../components/AboutShow";
import ShowMessages from "../../components/ShowMessages";
import AboutPerformer from "../../components/AboutPerformer";
import ReserveTicketModal from "../../components/ReserveTicketModal";
import AddShowMessageModal from "../../components/AddShowMessageModal/AddShowMessageModal";

const ShowPreview = () => {
  const { showId } = useParams();
  const { showData, showLoading, refetchShow } = useShows(showId);
  const { performerData, performerLoading } = usePerformers(
    showData?.performerId
  );
  const { categoryData, categoryLoading } = useCategories(showData?.categoryId);
  const { isOpen, onOpen, onClose } = useDisclosure();
  const {
    isOpen: isAddMessageOpen,
    onOpen: onAddMessageOpen,
    onClose: onAddMessageClose,
  } = useDisclosure();
  const base64Image = `data:image/jpeg;base64,${showData?.picture}`;

  return (
    <AuthenticatedLayout>
      <Flex flexWrap="wrap" h="100%" overflow={"auto"}>
        {showLoading || performerLoading || categoryLoading ? (
          <Flex
            w="100%"
            alignItems={"center"}
            justifyContent={"center"}
            h="100%"
          >
            <Icon as={BiLoader} boxSize={"20"} color={"purple.500"} />
          </Flex>
        ) : (
          <Flex flexDirection={"column"}>
            <Flex
              justifyContent={"start"}
              alignItems={"center"}
              w="100%"
              bgColor={"gray.700"}
              p={"5"}
              flex="1"
              maxH={"400px"}
            >
              <Flex flexDirection={"column"} gap="3" w="100%">
                <Flex justifyContent={"flex-start"} gap="2" w="100%">
                  <Text size={"sm"} color={"white"}>
                    {categoryData?.name}
                  </Text>
                  <Center height="20px">
                    <Divider orientation="vertical" />
                  </Center>
                  <Text size={"sm"} color={"white"}>
                    {categoryData?.description}
                  </Text>
                </Flex>
                <Heading as="h3" size="2xl" color={"white"}>
                  {showData?.name}
                </Heading>
              </Flex>
              <Flex
                w="100%"
                h="100%"
                justifyContent="center"
                alignItems="center"
              >
                <Image
                  src={base64Image}
                  alt={name}
                  borderRadius="lg"
                  style={{ width: "auto", height: "100%" }}
                  objectFit="cover"
                />
              </Flex>
            </Flex>
            <Flex flex="1" w="100%">
              <Tabs
                position="relative"
                variant="unstyled"
                bottom={"50px"}
                w="100%"
              >
                <TabList
                  style={{
                    outline: "none",
                    boxShadow: "none",
                    color: "white",
                    w: "100%",
                  }}
                >
                  <Tab>About Show</Tab>
                  <Tab>About Performer</Tab>
                </TabList>
                <TabIndicator
                  mt="-1.5px"
                  height="2px"
                  bg="purple.500"
                  borderRadius="1px"
                />
                <TabPanels>
                  <TabPanel w="100%">
                    <AboutShow show={showData} openModal={onOpen} />
                    <Box mt="10" w="100%">
                      <ShowMessages
                        showMessages={showData?.showMessages}
                        onAddMessageOpen={onAddMessageOpen}
                      />
                    </Box>
                  </TabPanel>
                  <TabPanel w="100%">
                    <AboutPerformer performer={performerData} />
                  </TabPanel>
                </TabPanels>
              </Tabs>
            </Flex>
          </Flex>
        )}
      </Flex>
      <ReserveTicketModal isOpen={isOpen} onClose={onClose} show={showData} />
      <AddShowMessageModal
        isOpen={isAddMessageOpen}
        onClose={onAddMessageClose}
        refetchShow={refetchShow}
        show={showData}
      />
    </AuthenticatedLayout>
  );
};

export default ShowPreview;
