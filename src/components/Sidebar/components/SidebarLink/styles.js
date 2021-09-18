import { makeStyles } from "@material-ui/styles";

export default makeStyles(theme => ({
  link: {
    textDecoration: "none",
    "&:hover, &:focus": {
      backgroundColor: theme.palette.background.default,
      borderTopLeftRadius: 30,
      borderBottomLeftRadius: 30
    },
  },
  linkActive: {
    backgroundColor: theme.palette.background.light,
    borderTopLeftRadius: 30,
    borderBottomLeftRadius: 30
  },
  linkNested: {
    "&:hover, &:focus": {
      backgroundColor: theme.palette.background.default,
      borderTopLeftRadius: 30,
      borderBottomLeftRadius: 30
    },
  },
  linkIcon: {
    minWidth: 0,
    marginRight: 10
  },
  linkIconActive: {
    color: theme.palette.secondary.light,
  },
  linkText: {
    padding: 0,
    color: theme.palette.text.secondary + "CC",
    transition: theme.transitions.create(["opacity", "color"]),
    fontSize: 14,
  },
  linkTextActive: {
    color: theme.palette.text.primary,
    fontWeight: "bold"
  },
  linkTextHidden: {
    opacity: 0,
  },
  nestedList: {
    paddingLeft: theme.spacing(1) + 20,
  },
  sectionTitle: {
    marginLeft: theme.spacing(4.5),
    marginTop: theme.spacing(2),
    marginBottom: theme.spacing(2),
  },
}));
