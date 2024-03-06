// ImageUpload.js
import { useState, useRef } from "react";
import { Box, Button, Flex, Image, Input, Text } from "@chakra-ui/react";
import { useFormikContext } from "formik";

const ImageUpload = ({ isEditFlow }) => {
  const [selectedImage, setSelectedImage] = useState(null);
  const fileInputRef = useRef(null);
  const { values, setFieldValue } = useFormikContext();

  const formatLabel = (propValue, textLimit) => {
    const length = propValue?.length;
    if (length >= textLimit) {
      return propValue?.substring(0, textLimit) + "...";
    }
    return propValue;
  };

  const handleImageChange = (event) => {
    const file = event.target.files[0];
    setSelectedImage(file);
    if (file) {
      const reader = new FileReader();

      reader.onloadend = () => {
        let base64 = reader.result;
        base64 = base64.replace(/^data:image\/[a-z]+;base64,/, "");
        setFieldValue("picture", base64);
      };

      // Read the image file as a Data URL
      reader.readAsDataURL(file);
    }
  };

  const handleButtonClick = () => {
    fileInputRef.current.click();
  };

  const handleUpload = () => {
    console.log("Selected image:", selectedImage);
  };

  const handleClear = () => {
    setSelectedImage(null);
    setFieldValue("picture", null);
  };

  if (isEditFlow && values?.picture) {
    return (
      <Box pb="3">
        <Flex
          w={"100%"}
          justifyContent={"space-between"}
          mb="2"
          alignItems={"center"}
        ></Flex>
        <Image
          src={`data:image/jpeg;base64,${values?.picture}`}
          alt="Selected"
          maxH="200px"
          mx="auto"
        />
      </Box>
    );
  }

  return (
    <Box>
      {!selectedImage && (
        <Box
          p={4}
          border="1px"
          borderColor="gray.100"
          borderRadius="lg"
          textAlign="center"
          position="relative"
          _before={{
            content: "''",
            position: "absolute",
            top: 0,
            right: 0,
            bottom: 0,
            left: 0,
            borderRadius: "inherit",
          }}
          onClick={handleButtonClick}
        >
          <Input
            type="file"
            onChange={handleImageChange}
            display="none"
            accept="image/*"
            ref={fileInputRef}
          />

          <Button colorScheme="purple" onClick={handleUpload}>
            Upload Image
          </Button>
        </Box>
      )}
      {selectedImage && (
        <Box pb="3">
          <Flex
            w={"100%"}
            justifyContent={"space-between"}
            mb="2"
            alignItems={"center"}
          >
            <Text mb={4}>{formatLabel(selectedImage.name, 15)}</Text>
            <Button colorScheme="red" onClick={handleClear} mt={2}>
              Clear
            </Button>
          </Flex>
          <Image
            src={URL.createObjectURL(selectedImage)}
            alt="Selected"
            maxH="200px"
            mx="auto"
          />
        </Box>
      )}
    </Box>
  );
};

export default ImageUpload;
