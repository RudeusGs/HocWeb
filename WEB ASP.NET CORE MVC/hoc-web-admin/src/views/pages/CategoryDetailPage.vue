<template>
  <GroupItem>
    <div class="row">
      <div class="col-sm-8">
        <div class="row" v-if="props.mode != 'create'">
          <label for="cateId" class="col-sm-2 col-form-label"
            >Mã danh mục</label
          >
          <div class="col-sm-10">
            <input
              class="form-control"
              v-model="selected.id"
              disabled
              id="cateId"
            />
          </div>
        </div>
        <div class="row mt-3">
          <label for="cateName" class="col-sm-2 col-form-label"
            >Tên danh mục</label
          >
          <div class="col-sm-10">
            <input
              class="form-control"
              v-model="selected.nameVN"
              id="cateName"
            />
          </div>
        </div>
        <div class="mt-3 row pe-2">
          <label for="formFile" class="form-label"
            >Ảnh hiện thị cho danh mục</label
          >
          <input
            class="form-control"
            type="file"
            id="formFile"
            @change="handleFileUpload"
          />
        </div>
        <div class="row mt-3" v-if="props.mode != 'create'">
          <label for="cateName" class="col-sm-2 col-form-label"
            >Tình trạng</label
          >
          <div class="col-sm-10">
            <select
              class="form-select"
              v-model="selected.status"
              aria-label="Default select example"
            >
              <option value="1">Đang hiển thị</option>
              <option value="2">Không hiển thị</option>
            </select>
          </div>
        </div>
      </div>
      <div class="col-sm-4 border border-white">
        <span v-if="!previewUrl">Xem trước</span>
        <img v-else :src="previewUrl" class="img-fluid" />
      </div>
    </div>
    <div class="row d-flex justify-content-center mt-4">
      <ButtonComponent
        :text="props.mode == 'create' ? 'Thêm' : 'Lưu'"
        @click="onSave"
        class-name="col-sm-2"
      />
    </div>
  </GroupItem>
</template>

<script setup lang="ts">
import { ref } from "vue";
import GroupItem from "@/components/GroupItem.vue";
import ButtonComponent from "@/components/ButtonComponent.vue";
import cateApi from "@/api/category.api";
import { getImage } from "@/components/helper";

const selected = ref({
  id: undefined,
  nameVN: "",
  image: "",
  status: "1",
  updatedDate: "",
  createdDate: "",
  deleteDate: "",
});

const file = ref<File>();
const previewUrl = ref<any>(null);

interface Props {
  mode?: string;
  onClose?: () => void;
}

const model = defineModel<any>();

const props = defineProps<Props>();

const onSave = async () => {
  if (props.onClose) {
    if (props.mode == "create") {
      if (!selected.value?.nameVN) {
        alert("Vui lòng điền tên danh mục");
        return;
      }
      const formData = new FormData();
      formData.append("nameVN", selected.value.nameVN);
      formData.append("name", selected.value.nameVN);
      if (file.value) {
        formData.append("file", file.value);
      }

      const response = await cateApi.AddNewCategory(formData);
      if (response && response.data.result.isSuccess) {
        props.onClose();
        return;
      }

      alert(response.data.result.message ?? "Lỗi");
    } else {
      const formData = new FormData();
      if (selected.value.id) {
        formData.append("id", selected.value.id);
      }
      const status = selected.value?.status == "2";
      formData.append("isDelete", status.toString());
      if (selected.value.nameVN) {
        formData.append("nameVN", selected.value.nameVN);
        formData.append("name", selected.value.nameVN);
      }
      if (file.value) {
        formData.append("file", file.value);
      }

      const response = await cateApi.UpdateCategory(formData);
      if (response && response.data.result.isSuccess) {
        props.onClose();
        return;
      }

      alert(response.data.result.message ?? "Lỗi");
    }
  }
};

const handleFileUpload = (event: Event) => {
  const input = event.target as HTMLInputElement;
  if (input.files && input.files.length > 0) {
    file.value = input.files[0];
    previewUrl.value = URL.createObjectURL(file.value);
  }
};

const loadInt = async () => {
  if (!model.value || !model.value.id) {
    return;
  }
  const response = await cateApi.getCategory(model.value.id);
  if (response && response.data.result.isSuccess) {
    const data = response.data.result.data;
    selected.value.id = data.id;
    selected.value.nameVN = data.nameVN;
    selected.value.image = data.image;
    selected.value.updatedDate = data.updatedDate;
    selected.value.createdDate = data.createdDate;
    selected.value.deleteDate = data.deleteDate;
    selected.value.status = data.deleteDate == undefined ? "1" : "2";
  } else {
    alert(response.data.result.message);
  }

  if (props.mode != "create" && selected.value.image) {
    previewUrl.value = await getImage(selected.value.image);
  }
};

loadInt();
</script>
