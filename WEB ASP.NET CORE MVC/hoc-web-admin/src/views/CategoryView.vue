<template>
  <div class="mb-3">
    <h4>Quản lý danh mục</h4>
  </div>
  <GroupItem title="Thông tin danh mục">
    <div class="row">
      <div class="row col-sm-4">
        <label for="cateId" class="col-sm-4 col-form-label">Mã danh mục</label>
        <div class="col-sm-8">
          <input
            class="form-control"
            v-model="selected.id"
            disabled
            id="cateId"
          />
        </div>
      </div>
      <div class="row col-sm-6">
        <label for="cateName" class="col-sm-3 col-form-label"
          >Tên danh mục</label
        >
        <div class="col-sm-9">
          <input
            class="form-control"
            v-model="selected.nameVN"
            disabled
            id="cateName"
          />
        </div>
      </div>
      <ButtonComponent
        text="Thêm"
        :onClick="onAdd"
        class-name="col-sm-1"
      />
      <ButtonComponent :disabled="updateStatus" text="Sửa" 
      :on-click="onUpdate" class-name="ms-2 col-sm-1" />
    </div>
  </GroupItem>
  <GroupItem title="Danh sách danh mục hiện có">
    <table class="table">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Tên danh mục</th>
          <th scope="col">Ngày tạo</th>
          <th scope="col">Tình trạng</th>
        </tr>
      </thead>
      <tbody>
        <tr
          v-for="(item, index) in categories"
          :key="index"
          @click="rowClick(item)"
        >
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ item.nameVN }}</td>
          <td>{{ item.createdDate }}</td>
          <td>{{ item.deleteDate ? "Không sử dung" : "Đang hiển thị" }}</td>
        </tr>
      </tbody>
    </table>
  </GroupItem>

  <DialogComponent
    :modelValue="isAddOpen"
    title="Thêm danh mục"
    @update:modelValue="isAddOpen = $event"
    @onDialogClose = "onDialogClose"
  >
  <template v-slot="{ onClose}">
    <CategoryDetailPage  :on-close="onClose" :mode="'create'" />
  </template>
  </DialogComponent>

  <DialogComponent
    :modelValue="isUpdateOpen"
    title="Cập nhật danh mục"
    @update:modelValue="isUpdateOpen = $event"
    @onDialogClose = "onDialogClose"
  >
  <template v-slot="{ onClose}">
    <CategoryDetailPage  :on-close="onClose" v-model="selected"/>
  </template>
  </DialogComponent>
</template>

<script setup lang="ts">
import { ref , computed} from "vue";
import GroupItem from "@/components/GroupItem.vue";
import ButtonComponent from "@/components/ButtonComponent.vue";
import DialogComponent from "@/components/DialogComponent.vue";
import CategoryDetailPage from "./pages/CategoryDetailPage.vue";
import cateApi from "@/api/category.api";

const categories = ref<any>([]);
const selected = ref({
  id: undefined,
  nameVN: "",
});
const isAddOpen = ref(false);
const isUpdateOpen = ref(false);

const onAdd = () => {
  isAddOpen.value = true;
};

const onDialogClose = async () => {
  await loadCategories();
};

const onUpdate = () => {
  isUpdateOpen.value = true;
};

const updateStatus = computed(()=>!selected.value.id)

const rowClick = (item: any) => {
  selected.value.id = item.id;
  selected.value.nameVN = item.nameVN;
};

const loadCategories = async () => {
  const response = await cateApi.getAllCategory();
  if (response && response.data.result.isSuccess) {
    categories.value = response.data.result.data
  } else {
    categories.value = [];
    alert(response.data.result.message);
  }
};

loadCategories();
</script>
