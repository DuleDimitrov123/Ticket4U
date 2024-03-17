const wrapper = {
  w: "full",
  h: "100%",
  // h: "100vh",
  // mt: "-60px",
  backgroundSize: "cover",
  backgroundPosition: "center center",
  alignItems: "flex-end",
  flexDirection: "column",
};

const vStack = {
  w: "full",
  justify: "center",
  px: { base: 4, md: 8 },
  bgGradient: "linear(to-r, blackAlpha.600, transparent)",
};

const content = {
  maxW: "2xl",
  align: "flex-start",
  spacing: 2,
  w: "full",
  display: "no-wrap",
  textAlign: "center",
};

const title = {
  color: "white",
  fontWeight: 700,
  lineHeight: 1.2,
  fontSize: { base: "3xl", md: "4xl" },
  textAlign: "center",
};

const button = {
  px: 8,
  colorScheme: "purple",
  rounded: "full",
  color: "white",
};

const styles = {
  wrapper,
  vStack,
  content,
  title,
  button,
};

export default styles;
