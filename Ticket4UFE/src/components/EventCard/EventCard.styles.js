const eventCard = {
  maxW: "sm",
  overflow: "hidden",
  m: 2,
  backgroundColor: "white",
  flexDir: "column",
  borderRadius: "6px",
  border: "1px",
  borderColor: "gray.100",
  _hover: {
    ".view_details": {
      transform: "scale(1.2)",
    },
  },
};

const seeAvailabilityBox = {
  justifyContent: "center",
  alignItems: "center",
  width: 44,
  height: "100%",
  right: 6,
  top: 0,
  position: "absolute",
  visibility: "hidden",
  opacity: 0,
  transition: "opacity 0.3s ease-in",
};

const styles = {
  eventCard,
  seeAvailabilityBox,
};

export default styles;
